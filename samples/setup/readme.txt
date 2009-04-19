1. Create a new database named NIntegrateDemo or 
   whatever name you want in SQL Server 2005, 2008 or Express
2. Run NIntegrateConfigAllInOneSetupScript.sql on the created database
3. Insert a row into [Server] table like 
   "insert into [Server] (ServerName, ServerAddress, Farm_id) 
   values ('put your server name here', 'localhost', 1)"
4. Run SimpleServiceProviderAndConsumer_data.sql on the created database 
   to initialize data for running SimpleServiceProviderAndConsumer sample
5. Open the NIntegrate_Samples.sln file, create the web applications in IIS when prompting
6. Change the connectionstring in web.config files pointing to your created database
7. Run the SimpleServiceConsumer web application


Note: 

By default, only http endpoints are enabled.

If you are using IIS 7, you could enable non-http endpoints 
by setting value of Active column of ServiceEndpoint_lnk table to true
(you could also enable non-tcp endpoints directly in the running Default.aspx
page of SimpleServiceConsumer web application).

After enabled/disabled endpoints, to make changes take effect, Application Pool recylcing 
is required before you re-run or refresh the running sample application pages. 