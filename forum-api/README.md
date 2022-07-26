

Contexte :
L’objectif de cet exercice est de créer une application de Forum permettant de créer des sujets dans lesquels nous pourront retrouver un espace de discussion sous forme de commentaires.

1)	Création du Projet 
Créer un nouveau projet .NET en choisissant le modèle :
 
Appeler le projet « forum-api »
2)	Définition de la base de données
Ce projet sera construit en « BD First » 
Ouvrir MySQL WorkBench et générer une base de données nommée « forum-db ».
Réflexion au niveau des tables :
	Un « topic » est un espace de discussion du forum contenant une date de création, une date de modification, un titre, le nom de la personne qui l’a créé et une liste de commentaires
	Un « comment » est une réponse à une discussion qui possède le nom de l’utilisateur qui l’a écrit, une date de création, une date de modification et un contenu.

3)	Générer les models et le contexte à l’aide de Entity
A l’aide de EntityFramework et Pomelo, générer le contexte et les modèles de l’application.
Ce lien peut vous aider : https://unaura.comfr/utiliser-mysql-avec-entity-framework-core/?lang=fr
4)	Générer les repositories et implémenter leurs méthodes
Générer la couche d’accès aux données avec les TopicRepository et CommentRepository et créer les méthodes CRUD (Create, Read, Update, Delete) pour chacune des entités.

5)	Générer les services et implémenter leurs méthodes
Générer le code métier de votre application en créant un TopicService et un CommentService.
Ne pas oublier de gérer les dates ainsi que la liste de commentaires vide pour la création d’un topic.
Ne pas oublier de gérer les dates ainsi que le lien avec un topic pour la création d’un message.
6)	Générer les controllers et implémenter leurs méthodes
Générer la liste des requêtes http permettant de rediriger l’utilisateur vers les bonnes méthodes du service.
Attention à respecter le standard REST pour les chemins de l’URL.
7)	Tester chacune des méthodes des services
Chacune des méthodes de vos services doit être testé en prenant en compte toutes les possibilités. 
Par exemple pour un findById : 
	L’id 2 nous retourne le Topic qui porte l’id 2 avec tous ses commentaires
	L’id 7 lève une exception puisque le Topic qui porte l’id 7 n’existe pas

8)	Créer un service WordFilterService
Récupérer le fichier « insults.txt » qui est fourni en pièce jointe.
Créer un tableau de String[] nommée « banWords » et importer chacun des mots présents dans le fichier « insults.txt » dans le tableau à la construction du service.
Créer une méthode permettant de filtrer le contenu d’une chaîne de caractère :
	Est-ce que la chaîne de caractère contient une insulte ou un mot vulgaire ?
	Si oui, remplacer le mot par des étoiles (ex : débile  d****e)

9)	Tester le WordFilterService
Comme pour les autres services, WordFilterService fait partie du code métier de l’application et chacune des méthodes que l’on vient de créer doivent être testées.
10)	Mettre à jour les méthodes et tests unitaires de CommentsService et TopicService 
Injecter le WordFilterService dans les TopicService et CommentService afin qu’aucune insulte n’apparaisse dans le titre du topic et dans le contenu des commentaires.
Mettez à jour ou rajouter des tests unitaires pour vérifier qu’aucune insulte ne peut apparaitre dans les deux entités.

11)	Mettre en place le frontend avec Angular
Notre API Rest est prête à l’emploi alors nous pouvons générer une application Angular pour la partie client.
