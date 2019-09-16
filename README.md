# Invisionware.DirectoryServices
Invisionware library for working Directory Services 

## Usage
See sample code for how to use the DN and RDN classes

# Invisionware.DirectoryServices.LDAP
Invisionware library for working LDAP in an easier fashon

## Usage

```
	var connection = new LdapServerConnection { Server = "<LDAP Server Name or IP>" }; 
	var connectionManager = new LdapConnectionManager(connection);

	var accountManager = connectionManager.CreateAccountManager();

	var results = accountManager.FindUsers("sanderson");

	Console.WriteLine(results.First().AccountName);
```
