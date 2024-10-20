# Tennis Player Statistics API

## Objectif
Créer une API simple permettant de retourner les statistiques des joueurs de tennis. 

## Tâches réalisées
1. **Endpoint pour récupérer la liste des joueurs** : Retourne les joueurs triés du meilleur au moins bon.
2. **Endpoint pour récupérer les informations d’un joueur par ID** : Retourne les informations d’un joueur spécifique en utilisant son identifiant.
3. **Endpoint pour récupérer des statistiques** : Retourne :
   - Le pays avec le plus grand ratio de parties gagnées.
   - L'IMC moyen de tous les joueurs.
   - La médiane de la taille des joueurs.
4. **Déploiement sur le Cloud** : L'API a été déployée sur Azure.

## Lien déployé
L'API est accessible à l'adresse suivante : (https://tennisplayerstats-akg9eychgda0hcg4.westeurope-01.azurewebsites.net).
#Tâche 2: URL : https://tennisplayerstats-akg9eychgda0hcg4.westeurope-01.azurewebsites.net/id
#Tâche 3: URL : https://tennisplayerstats-akg9eychgda0hcg4.westeurope-01.azurewebsites.net/stats

## Cloner et installer le projet
Pour cloner et installer ce projet, assurez-vous d'avoir [Git](https://git-scm.com/) et [.NET Core](https://dotnet.microsoft.com/download) installés sur votre machine. 

Exécutez les commandes suivantes dans votre terminal :

```bash
## Cloner le dépôt
git clone https://github.com/achrafat/TennisPlayerStats.git

##Lancer et tester l'API
dotnet run

##Exécution des tests
dotnet test
