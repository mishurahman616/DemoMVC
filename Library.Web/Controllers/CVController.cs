using Microsoft.AspNetCore.Mvc;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Library.Web.Models;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.css;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;

namespace Library.Web.Controllers
{
    public class CVController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult GeneratePDF()
        {
            // HTML content of the CV
            var cvmodel = new CVModel();
            string htmlContent = cvmodel.DemoCV; // Paste your HTML content here


            // Create a PDF document
            MemoryStream memoryStream = new MemoryStream();
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();
            var cssText = "body {\r\n            font-family: Arial, sans-serif;\r\n            margin: 0;\r\n            padding: 0;\r\n            background-color: #f4f4f4;\r\n            color: #333;\r\n        }\r\n\r\n        header {\r\n            background-color: #35424a;\r\n            color: #ffffff;\r\n            padding: 10px 0;\r\n            text-align: center;\r\n        }\r\n\r\n        .container {\r\n            max-width: 960px;\r\n            margin: 20px auto;\r\n            padding: 20px;\r\n            background-color: #ffffff;\r\n            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);\r\n        }\r\n\r\n        h1 {\r\n            margin-top: 0;\r\n        }\r\n\r\n        h3 {\r\n            color: #35424a;\r\n        }\r\n\r\n        .section {\r\n            margin-bottom: 20px;\r\n         }\r\n\r\n            .section h4 {\r\n                margin-top: 0;\r\n            }\r\n\r\n            .section ul {\r\n                list-style-type: disc;\r\n                margin-left: 20px;\r\n                padding-left: 0;\r\n            }\r\n\r\n        .reference-container {\r\n            display: flex;\r\n            justify-content: space-between;\r\n            flex-wrap: wrap;\r\n        }\r\n\r\n        .reference {\r\n            width: 48%; /* Adjust the width as needed */\r\n            margin-bottom: 20px;\r\n        }";
            var css = "body{font-family: Arial, sans-serif;margin: 0;padding: 0;background-color: #f4f4f4;color: #333}header{background-color: #35424a;color: #ffffff;padding: 10px 0;text-align: center}.container{max-width: 960px;margin: 20px auto;padding: 20px;background-color: #ffffff;box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1)}h1{margin-top: 0}h3{color: #35424a}.section{margin-bottom: 20px}.section h4{margin-top: 0}.section ul{list-style-type: disc;margin-left: 20px;padding-left: 0}.reference-container{display: flex;justify-content: space-between;flex-wrap: wrap}.reference{width: 48%;margin-bottom: 20px}";


            // Convert HTML content to PDF
            using (var cssMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(cssText)))
            {
                using (var htmlMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(htmlContent)))
                {
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, htmlMemoryStream, cssMemoryStream);

                }
            }

            document.Close();

            // Return the PDF as a file
            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();
            return File(bytes, "application/pdf", "CV.pdf");
        }
        public IActionResult PDF()
        {
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new Stream(file));
            document.Open();
            CSSResolver cssResolver = new StyleAttrCSSResolver();
            CssFile cssFile = XMLWorkerHelper.getCSS(new ByteArrayInputStream(CSS_STYLE.getBytes()));
            cssResolver.addCss(cssFile);
            // HTML  
            HtmlPipelineContext htmlContext = new HtmlPipelineContext(null);
            htmlContext.setTagFactory(Tags.getHtmlTagProcessorFactory());
            // Pipelines  
            PdfWriterPipeline pdfFile = new PdfWriterPipeline(document, writer);
            HtmlPipeline html = new HtmlPipeline(htmlContext, pdfFile);
            CssResolverPipeline css = new CssResolverPipeline(cssResolver, html);
            // XML Worker  
            XMLWorker worker = new XMLWorker(css, true);
            XMLParser p = new XMLParser(worker);
            p.parse(new ByteArrayInputStream(HTML.getBytes()));
            document.close();
        }
    }
}
