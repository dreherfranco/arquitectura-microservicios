#Imagenes:
   - dreherfranco/category-service
   - dreherfranco/article-service

#Hacer migracion de bases de datos

Activar clave secreta para mssql:
kubectl create secret generic mssql --from-literal=SA_PASSWORD="pa55w0rd!"

#Ingress:
   - Para configurar Ingress nginx se debe ejecutar el comando:
      kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.3.0/deploy/static/provider/cloud/deploy.yaml
   - En el archivo "hosts" de nuestro Sistema Operativo se debe agregar (en windows    C:\Windows\System32\drivers\etc):
      127.0.0.1 microservicios-dreherfranco.com