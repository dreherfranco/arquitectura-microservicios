#Imagenes:
   - dreherfranco/category-service

#Hacer migracion de bases de datos

Activar clave secreta para mssql:
kubectl create secret generic mssql --from-literal=SA_PASSWORD="pa55w0rd!"