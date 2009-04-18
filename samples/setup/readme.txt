1. create a new database named NIntegrateDemo or 
   whatever name you want in SQL Server 2005, 2008 or Express
2. run NIntegrateConfigAllInOneSetupScript.sql on the created database
3. insert a row into [Server] table by 
   "insert into [Server] (ServerName, ServerAddress, Farm_id) 
   values ('put your server name here', 'localhost', 1)"
4. run SimpleServiceProviderAndConsumer_data.sql on the created database 
   to initialize data for running SimpleServiceProviderAndConsumer sample
5. open the sample .sln file, create the web applications in IIS when prompting
6. change the connection string in web.config files pointing to your created database
7. run the SimpleServiceConsumer web application


Note: 

by default, only http endpoints are enabled, 
if you are using IIS 7, you could enable non-http endpoints 
by setting value of Active column of ServiceEndpoint_lnk table to true