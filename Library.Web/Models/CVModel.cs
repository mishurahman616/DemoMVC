namespace Library.Web.Models
{
    public class CVModel
    {
        public CVModel() { }
        public List<(string selector, string attribute, string value)> CVStyles { get; set; } = new List<(string selector, string attribute, string value)>()
        {
            ("body", "font-family", "Arial, sans-serif"),
("body", "margin", "0"),
("body", "padding", "0"),
("body", "background-color", "#f4f4f4"),
("body", "color", "#333"),

("header", "background-color", "#35424a"),
("header", "color", "#ffffff"),
("header", "padding", "10px 0"),
("header", "text-align", "center"),

(".container", "max-width", "960px"),
(".container", "margin", "20px auto"),
(".container", "padding", "20px"),
(".container", "background-color", "#ffffff"),
(".container", "box-shadow", "0px 0px 10px rgba(0, 0, 0, 0.1)"),

("h1", "margin-top", "0"),

("h3", "color", "#35424a"),

(".section", "margin-bottom", "20px"),
(".section h4", "margin-top", "0"),
(".section ul", "list-style-type", "disc"),
(".section ul", "margin-left", "20px"),
(".section ul", "padding-left", "0"),

(".reference-container", "display", "flex"),
(".reference-container", "justify-content", "space-between"),
(".reference-container", "flex-wrap", "wrap"),

(".reference", "width", "48%"),
(".reference", "margin-bottom", "20px")

        };
        


        public string DemoCV { get; set; } = @"<!DOCTYPE html>
<html lang=\""en\"">
<head>
    <meta charset=\""UTF-8\"" />
    <meta name=\""viewport\"" content=\""width=device-width, initial-scale=1.0\"" />
    <title>ASP.NET Software Developer CV</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f4f4f4;
            color: #333;
        }

        header {
            background-color: #35424a;
            color: #ffffff;
            padding: 10px 0;
            text-align: center;
        }

        .container {
            max-width: 960px;
            margin: 20px auto;
            padding: 20px;
            background-color: #ffffff;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
        }

        h1 {
            margin-top: 0;
        }

        h3 {
            color: #35424a;
        }

        .section {
            margin-bottom: 20px;
         }

            .section h4 {
                margin-top: 0;
            }

            .section ul {
                list-style-type: disc;
                margin-left: 20px;
                padding-left: 0;
            }

        .reference-container {
            display: flex;
            justify-content: space-between;
            flex-wrap: wrap;
        }

        .reference {
            width: 48%; /* Adjust the width as needed */
            margin-bottom: 20px;
        }
    </style>
</head>
<body>
    <header>
        <h1>John Doe</h1>
        <p>ASP.NET Software Developer</p>
    </header>
    <div class=\""container\"">
        <div class=\""section\"">
             <h3>Summary</h3><hr />
            <p>Experienced ASP.NET developer with a strong background in web application development. Skilled in creating responsive and efficient solutions using C# and related technologies.</p>
        </div>
        <div class=\""section\"">
            <h3>Skills</h3><hr />
            <ul>
                <li>C#</li>
                <li>ASP.NET</li>
                <li>MVC</li>
                <li>Entity Framework</li>
                <li>SQL Server</li>
                <li>HTML5 & CSS3</li>
                <li>JavaScript & jQuery</li>
                <li>RESTful APIs</li>
                <li>Version Control (Git)</li>
            </ul>
        </div>
        <div class=\""section\"">
            <h3>Experience</h3><hr />
            <h4>Software Developer - ABC Tech Solutions</h4>
            <p><strong>Date:</strong> January 2020 - Present</p>
            <ul>
                <li>Developed and maintained ASP.NET web applications for clients, resulting in increased user engagement.</li>
                <li>Collaborated with cross-functional teams to design and implement new features.</li>
                <li>Optimized database queries and improved application performance.</li>
            </ul>

            <h4>Junior Developer - XYZ Software</h4>
            <p><strong>Date:</strong> May 2018 - December 2019</p>
            <ul>
                <li>Assisted in the development of web applications using ASP.NET MVC.</li>
                <li>Participated in code reviews and provided valuable feedback to the team.</li>
            </ul>
        </div>
        <div class=\""section\"">
            <h3>Education</h3><hr />
            <p><strong>Bachelor of Science in Computer Science</strong></p>
            <p>University of Example, City, Country</p>
            <p><strong>Date:</strong> May 2018</p>
        </div>
        <div class=\""section\"">
            <h3>Projects</h3><hr />
            <h4>E-commerce Website - TechStore</h4>
            <p><strong>Date:</strong> 2022</p>
            <ul>
                <li>Developed a responsive e-commerce website using ASP.NET MVC and Entity Framework.</li>
                <li>Integrated payment gateways for seamless online transactions.</li>
                <li>Implemented product recommendation engine using collaborative filtering.</li>
            </ul>

            <h4>Inventory Management System</h4>
            <p><strong>Date:</strong> 2021</p>
            <ul>
                <li>Designed and implemented an inventory management system for a retail client.</li>
                <li>Utilized ASP.NET Web API to create RESTful endpoints for inventory operations.</li>
                <li>Integrated with third-party logistics APIs for real-time tracking.</li>
            </ul>
        </div>

        <div class=\""section\"">
            <h3>Certifications</h3><hr />
            <ul>
                <li>Microsoft Certified: Azure Developer Associate</li>
                <li>Certified ScrumMaster</li>
            </ul>
        </div>

        <div class=\""section\"">
            <h3>Languages</h3><hr />
            <ul>
                <li>English (Fluent)</li>
                <li>Spanish (Intermediate)</li>
            </ul>
        </div>

        <div class=\""section\"">
            <h3>Contact</h3><hr />
            <p><strong>Email:</strong> john.doe@example.com</p>
            <p><strong>LinkedIn:</strong> linkedin.com/in/johndoe</p>
            <p><strong>GitHub:</strong> github.com/johndoe</p>
        </div>
        <div class=\""section\"">
            <h3>Publications</h3><hr />
            <h4>Article: \""Best Practices in ASP.NET Web Development\""</h4>
            <p><strong>Date:</strong> 2022</p>
            <p>Published in <em>Tech Journal</em>, discussing modern practices for building robust ASP.NET web applications.</p>

            <h4>Book: \""Mastering C# and ASP.NET MVC\""</h4>
            <p><strong>Date:</strong> 2021</p>
            <p>Co-authored a comprehensive guide on C# programming and ASP.NET MVC development.</p>
        </div>

        <div class=\""section\"">
            <h3>Volunteer Work</h3><hr />
            <h4>Mentor - CodeSprint Youth Coding Club</h4>
            <p><strong>Date:</strong> 2020 - Present</p>
            <p>Providing guidance and support to young students interested in programming and web development.</p>

            <h4>Open Source Contributions</h4>
            <p><strong>Date:</strong> Ongoing</p>
            <p>Contributing to various open-source projects on GitHub, focusing on ASP.NET and C# libraries.</p>
        </div>

        <div class=\""section\"">
            <h3>Achievements</h3><hr />
            <ul>
                <li>Recipient of the \""Innovative Developer of the Year\"" award at DeveloperCon 2022.</li>
                <li>Featured speaker at ASP.NET Conference 2021, discussing performance optimization techniques.</li>
            </ul>
        </div>

        <div class=\""section\"">
            <h3>References</h3><hr />
            <p>Available upon request.</p>
        </div>
        <div class=\""section\"">
            <h3>References</h3><hr />
            <div class=\""reference-container\"">
                <div class=\""reference\"">
                    <h4>Jane Smith</h4>
                    <p><strong>Position:</strong> Senior Software Engineer</p>
                    <p><strong>Company:</strong> ABC Tech Solutions</p>
                    <p><strong>Email:</strong> jane.smith@example.com</p>
                    <p><strong>Phone:</strong> (123) 456-7890</p>
                    <p><strong>Relationship:</strong> Former Supervisor</p>
                </div>
                <div class=\""reference\"">
                    <h4>John Doe</h4>
                    <p><strong>Position:</strong> Lead Developer</p>
                    <p><strong>Company:</strong> XYZ Software</p>
                    <p><strong>Email:</strong> john.doe@example.com</p>
                    <p><strong>Phone:</strong> (987) 654-3210</p>
                    <p><strong>Relationship:</strong> Collaborator on Project X</p>
                </div>
            </div>
        </div>
        <button class=\""mb-5\"" id=\""generate-pdf\"">Generate PDF</button>

    </div>
</body>
</html>

";
    }
}
