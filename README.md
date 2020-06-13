# testNetCoreSwagger
Para correr el proyecto solo necesita cambiar en el Json de configuración el string de conexión a base de datos, se usa SQL Server.
Es posible tambien que deba instalar Net Core 2.2. Ademas la API proporciona dos endpoint que no tienen seguridad alguna, 
se espera que usando estos pueda crear el primer rol de la aplicacion, este debe ser "Admin" y el primer usuario asociado a este rol,
luego loguearse (la contraseña es el nombre de usuario todo en minuscula) y probar los endpoints.
