## MSGraph_Tooklit

Demonstrates basic authentication and query on the Microsoft Graph API.

Authentication runs using ``Microsoft.Identity.Client`` leveraging profiles of currently logged on user.

Queries of the Graph give access to a range of enterprise level information including SharePoint, and MS Graph as demonstrated here.

Microsoft Graph REST API beta is documented [here](https://learn.microsoft.com/en-us/graph/api/overview?view=graph-rest-beta&preserve-view=true)

Queries are dependent on an app registered via Azure Active Directory and granted certain access permissions to specific end points. 

Current usage is based on Future-Test-GraphAPI. 

Current permissions shown below additional permissions would require contact with Cloud Services and Information Technology
![permission](permissions.JPG)

MSGraphAdapter checks for is there is a current token.

If no token a UWP app is called which depending on the sign in method either gets the user that signed into Windows or brings up a UI for the user to provide credentials.

While this is currently bound to MSGraph_Toolkit the MSGraph_Auth part should be made generic for use by other applications.

A further TODO is make an asynchronous Pull method maybe non- Bhom?
