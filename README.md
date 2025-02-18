# Configuración de Kubernetes con Minikube

Esta guía te ayudará a desplegar y probar dos APIs (`api-a` y `api-b`) en un clúster de Kubernetes utilizando Minikube.

## Requisitos previos

- [Minikube](https://minikube.sigs.k8s.io/docs/) instalado y configurado.
- [Kubectl](https://kubernetes.io/docs/tasks/tools/) instalado y configurado.

## Pasos para desplegar las APIs

1. **Iniciar Minikube**

   ```bash
   minikube start
   ```

   Este comando inicia el clúster de Minikube.

2. **Desplegar las APIs**

   - Aplica la configuración de despliegue y servicio para `api-a`:
     ```bash
     kubectl apply -f api-a-deployment.yaml
     ```
   - Aplica la configuración de despliegue y servicio para `api-b`:
     ```bash
     kubectl apply -f api-b-deployment.yaml
     ```

3. **Verificar el estado de los Pods**
   Para asegurarte de que los pods se están ejecutando correctamente, usa:

   ```bash
   kubectl get pods
   ```

   Busca los pods `api-a` y `api-b` en la lista y asegúrate de que su estado sea `Running`.

4. **Probar los endpoints de las APIs**

   Usa port-forwarding para exponer las APIs localmente para pruebas:

   - Para `api-a`:
     ```bash
     kubectl port-forward service/api-a-service 8080:8080
     ```
   - Para `api-b`:
     ```bash
     kubectl port-forward service/api-b-service 8081:8080
     ```

   Esto redirige los puertos locales a los servicios respectivos en el clúster. `api-a` estará disponible en `http://localhost:8080`, y `api-b` estará disponible en `http://localhost:8081`.

5. **Probar la comunicación entre las APIs**

   Asegúrate de que `api-b` esté configurada para llamar a `api-a` utilizando su nombre de servicio de Kubernetes (`api-a-service`) y puerto (`8080`). Puedes probar la API `api-b` usando herramientas como Postman, Curl o Swagger para asegurarte de que se comunique correctamente con `api-a`.

## Resolución de problemas

- **Problemas con el estado de los Pods**:
  Si un pod no se está ejecutando, revisa los logs utilizando:

  ```bash
  kubectl logs <pod-name>
  ```

- **Problemas de resolución de DNS**:
  Asegúrate de que las APIs utilicen los nombres de servicio correctos de Kubernetes para la comunicación interna.

  ```
  http://api-a-service:8080
  ```

- **Conflictos con el port-forwarding**:
  Asegúrate de que los puertos en tu máquina local estén libres antes de ejecutar `kubectl port-forward`.

## Limpieza

Para eliminar los recursos creados:

```bash
kubectl delete -f api-a-deployment.yaml
kubectl delete -f api-b-deployment.yaml
```

Para detener Minikube:

```bash
minikube stop
```

#
