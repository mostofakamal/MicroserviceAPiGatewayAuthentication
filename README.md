# Microservice Api Gateway

Api Gateway is called the Back end for the front end (BFF) . In Microservice architecture it plays a crucial role in performing cross cutting and infrastructural concerns e.g. Authentication, Curcuit breaking, Retry policy, Aggregation and so on. This project demonstrates basic API gateway for a microservice and how authentication can be done in API Gateway set up in Microservice architecture . The project also includes Kuebernetes deployment yaml files for deploying these applications in a kubernetes cluster

## The project is developed with following tools and technologies

- ASP.NET Core 3.1
- C#
- Ocelot API Gateway
- Identity Server 4 as Open ID Connect Auth Server
- Kubernetes

## How to run:

You need docker and Kubernetes installed.
Create the docker image first and then deploy them to your kubernetes cluster.

## Build Docker image:

`docker-compose build`

## Deploying to Kubernetes

Move to the directory Kubernetes deploy and run the following command <br/>

Create Deployment:

`kubectl apply -f AllDeployment.yml` <br/>

Create Service: <br/>

`kubectl apply -f AllService.yml`

# Solution Description :

## OcelotApiGw

It is the API gateway which uses Ocelot . There is a ocelot.json file which contains the routing information. Detail documentation can be found [here](https://ocelot.readthedocs.io/en/latest/introduction/gettingstarted.html)

## TestIdentityServer

It contains the OAuth Open ID Connect implementation using Identity Server 4. There is a Config class in the root directory where some sample Clients has been set up which can be used to get the token using different grant types

## Sample API

This is the Sample .NET Core API which we are routing with API Gateway and protecting with Identity Server Authentication
