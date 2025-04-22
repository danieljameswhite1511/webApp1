namespace Application.Users.EmailTemplates;

public class HtmlTemplates
{
    public static string ConfirmEmailTemplate(string url)
    {
        return $"""
                    <html>
                    <head>
                    </head>
                        <body>
                            <p>Confirm your email address </p>
                            <a href="{url}">Confirm email</a>
                        </body>     
                    </html>
                """;
    }
}