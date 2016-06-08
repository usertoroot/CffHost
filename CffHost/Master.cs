using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CffHost
{
    public class Master
    {
        private HttpListener m_listener;
        private Dictionary<int, object> m_cffObjects = new Dictionary<int, object>();
        private int m_drawGenerator = 0;

        public Master()
        {

        }

        public void Run()
        {
            Regex typeRegex = new Regex("_\\d{4}");

            Console.WriteLine("Loading cff files...");            
            foreach (var t in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (t.Name.Length != 5)
                    continue;

                if (!typeRegex.IsMatch(t.Name))
                    continue;

                int id = Convert.ToInt32(t.Name.Substring(1, 4));

                using (Stream s = File.OpenRead(Path.Combine("cff", id.ToString().PadLeft(4, '0'))))
                using (BinaryReader reader = new BinaryReader(s))
                {
                    try
                    {
                        object o = reader.ReadStructure(t);
                        m_cffObjects.Add(id, o);
                        Console.WriteLine("Cff {0} loaded.", id);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Cff {0} failed to process: {1}.", id, e.Message);
                    }
                }
            }

            Console.WriteLine("Creating HttpListener on 8080...");

            m_listener = new HttpListener();
            m_listener.Prefixes.Add("http://+:8080/");

            m_listener.Start();

            Console.WriteLine("Waiting for request...");
            Regex idRegex = new Regex("\\d{4}");

            while (true)
            {
                HttpListenerContext context = null;

                try
                {
                    context = m_listener.GetContext();
                    var request = context.Request;
                    var response = context.Response;
                    
                    string url = request.RawUrl.ToLower();

                    int index = url.IndexOf('?');
                    if (index > 0)
                        url = url.Substring(0, index);

                    url = Path.GetInvalidFileNameChars().Aggregate(url, (current, c) => current.Replace(new string(c, 1), ""));

                    if (url == "")
                        url = "index.html";

                    Console.WriteLine("Received request '{2}' from {0}:{1}.", request.RemoteEndPoint.Address, request.RemoteEndPoint.Port, url);

                    if (url.StartsWith("data") && idRegex.IsMatch(url))
                    {
                        int id = Convert.ToInt32(url.Substring(4, 4));
                        var o = m_cffObjects[id];
                        var t = o.GetType();

                        var entryFieldType = t.GetFields()[0];
                        var entryArray = (Array)entryFieldType.GetValue(o);

                        StringBuilder builder = new StringBuilder(40960);
                        StringWriter sw = new StringWriter(builder);

                        using (JsonWriter writer = new JsonTextWriter(sw))
                        {
                            writer.WriteStartObject();

                            writer.WritePropertyName("draw");
                            writer.WriteValue(++m_drawGenerator);

                            writer.WritePropertyName("recordsTotal");
                            writer.WriteValue(entryArray.Length);

                            string search = request.QueryString["search[value]"];

                            List<object> entries = new List<object>(entryArray.Length);
                            for (int i = 0; i < entryArray.Length; i++)
                            {
                                if (FieldContains(search, entryArray.GetValue(i)))
                                    entries.Add(entryArray.GetValue(i));
                            }

                            writer.WritePropertyName("recordsFiltered");
                            writer.WriteValue(entries.Count);

                            writer.WritePropertyName("data");
                            writer.WriteStartArray();

                            int length = Convert.ToInt32(request.QueryString["length"]);
                            int start = Convert.ToInt32(request.QueryString["start"]);
                            if (start < 0)
                                start = 0;

                            int end = start + length;
                            if (end > entries.Count)
                                end = entries.Count;

                            for (int i = start; i < end; i++)
                            {
                                writer.WriteStartArray();

                                var oa = entries[i];

                                foreach (var field in oa.GetType().GetFields())
                                {
                                    if (field.FieldType.Name.EndsWith("Container"))
                                    {
                                        var oc = field.GetValue(oa);

                                        foreach (var e in field.FieldType.GetFields())
                                        {
                                            var so = e.GetValue(oc);
                                            writer.WriteValue(GetValue(so));
                                        }

                                        continue;
                                    }

                                    writer.WriteValue(GetValue(field.GetValue(oa)));
                                }

                                writer.WriteEnd();
                            }

                            writer.WriteEnd();
                            writer.WriteEndObject();
                        }

                        byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
                        response.ContentType = "application/json";
                        response.ContentLength64 = data.Length;
                        response.OutputStream.Write(data, 0, data.Length);
                    }
                    else if (idRegex.IsMatch(url))
                    {
                        int id = Convert.ToInt32(url);
                        var o = m_cffObjects[id];
                        var t = o.GetType();

                        var entryFieldType = t.GetFields()[0];
                        var entryArray = (Array)entryFieldType.GetValue(o);

                        StringBuilder builder = new StringBuilder(4096);

                        foreach (var field in entryArray.GetType().GetElementType().GetFields())
                        {
                            var f = field;
                            if (f.FieldType.Name.EndsWith("Container"))
                            {
                                foreach (var e in f.FieldType.GetFields())
                                    builder.AppendFormat("<th>{0}</th>", e.Name);

                                continue;
                            }

                            builder.AppendFormat("<th>{0}</th>", f.Name);
                        }

                        string tableHeader = builder.ToString();

                        StringBuilder hrefBuilder = new StringBuilder(8096);
                        
                        foreach (var cff in m_cffObjects.Keys)
                            hrefBuilder.Append(String.Format("<a href=\"{0}\">{0}</a> ", cff));

                        string templateHtml = "<head><link rel=\"stylesheet\" type=\"text/css\" href=\"jquery.dataTables.min.css\"><script type=\"text/javascript\" src=\"jquery.js\"></script><script type=\"text/javascript\" src=\"jquery.dataTables.js\"></script><script type=\"text/javascript\">$(document).ready(function() {$('#example').DataTable({\"processing\": true,\"serverSide\": true,\"ajax\": \"data{id}\"});});</script></head><body><div>{cffFiles}</div><div><table id=\"example\" class=\"display\" cellspacing=\"0\" width=\"100%\"><thead><tr>{tableHeader}</tr></thead><tfoot><tr>{tableHeader}</tr></tfoot></table></div></body>";
                        string html = templateHtml.Replace("{tableHeader}", tableHeader);
                        html = html.Replace("{id}", id.ToString());
                        html = html.Replace("{cffFiles}", hrefBuilder.ToString());

                        byte[] data = Encoding.UTF8.GetBytes(html);
                        response.ContentType = "text/html";
                        response.ContentLength64 = data.Length;
                        response.OutputStream.Write(data, 0, data.Length);
                    }
                    else
                    {
                        switch (url)
                        {
                            default:
                                {
                                    string extension = Path.GetExtension(url);
                                    response.ContentType = MimeTypeMap.GetMimeType(extension);

                                    byte[] data = File.ReadAllBytes(Path.Combine("data", url));
                                    response.ContentLength64 = data.Length;
                                    response.OutputStream.Write(data, 0, data.Length);
                                    break;
                                }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception failed to process request: '{0}'.", e.Message);
                }
                finally 
                {
                    if (context != null)
                        context.Response.OutputStream.Close();
                }
            }
        }

        public bool FieldContains(string needle, object o)
        {
            var t = o.GetType();

            if (t == typeof(BfString) || t == typeof(BfWString))
                return o.ToString().ToLower().Contains(needle.ToLower());

            if (t.IsArray)
            {
                Array a = (Array)o;

                for (int i = 0; i < a.Length; i++)
                {
                    if (FieldContains(needle, a.GetValue(i)))
                        return true;
                }

                return false;
            }
            else if (t.IsCustomValueType())
            {
                foreach (var field in t.GetFields())
                {
                    if (FieldContains(needle, field.GetValue(o)))
                        return true;
                }
                
                return false;
            }
            else
                return o.ToString().ToLower().Contains(needle.ToLower());
        }
        
        public string GetValue(object o)
        {
            var t = o.GetType();

            if (t == typeof(BfString) || t == typeof(BfWString))
                return o.ToString();

            if (t.IsArray)
            {
                Array a = (Array)o;
                StringBuilder builder = new StringBuilder(1024);

                builder.Append("<ul>");
                for (int i = 0; i < a.Length; i++)
                    builder.AppendFormat("<li>{1}[{2}]{0}</li>", GetValue(a.GetValue(i)), o.GetType().GetElementType().Name, i);
                builder.Append("</ul>");

                return builder.ToString();
            }
            else if (t.IsCustomValueType())
            {
                StringBuilder builder = new StringBuilder(1024);

                builder.Append("<ul>");
                foreach (var field in t.GetFields())
                    builder.AppendFormat("<li>{0}</li>", GetValue(field.GetValue(o)));
                builder.Append("</ul>");

                return builder.ToString();
            }
            else
                return o.ToString();
        }

        public void WriteValue(JsonWriter writer, object o)
        {
            var t = o.GetType();

            if (t.IsArray)
            {
                Array a = (Array)o;

                StringBuilder builder = new StringBuilder(1024);
                for (int i = 0; i < a.Length; i++)
                    builder.AppendFormat("<li></li>");
            }
        }
    }
}
