To run this application in VSCode, you have some dependencies:<br />
&emsp;-Azure Functions Core Tools development runtime extention in VSCode <br />
&emsp;-Azure Functions extension for VSCode <br />
&emsp;-.NET Core SDK on the machine <p></p>

Environment variables:<br />
&emsp;mongodbURI: mongodb database connection string<br />
&emsp;databaseName: database name<br />
&emsp;userCollectionName: collection name<p></p>
	<p></p>

After this, to run the application in VSCode: <br />
&emsp;-open the integrated terminal <br />
&emsp;-move to the root directory containing the application <br />
&emsp;-execute the command "func start" to run the application <p></p>
	<p></p>

There is an instance of this application deployed in Azure. These are the web-api endpoints:<br />
&emsp;-<a href="https://users23771.azurewebsites.net/api/getuserbyid/{id}">https://users23771.azurewebsites.net/api/getuserbyid/{id}</a><br />
&emsp;-<a href="https://users23771.azurewebsites.net/api/getallusers">https://users23771.azurewebsites.net/api/getallusers</a><br />
&emsp;-<a href="https://users23771.azurewebsites.net/api/postusers">https://users23771.azurewebsites.net/api/postusers</a><br />
&emsp;-<a href="https://users23771.azurewebsites.net/api/deleteusers">https://users23771.azurewebsites.net/api/deleteusers</a>

