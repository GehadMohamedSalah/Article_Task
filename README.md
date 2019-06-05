# Article_Task
steps how to build the project and make it up and running
1-create database on sql server 2017
2-generate script from created database
3-create new asp.net mvc project on visual studio 2015
4-use database script to create database on visual studio
5-add  ADO.NET Entity Data Model in Model folder to work database first
6-create 3 layout(_HomeLayout , _AdminLayout , _VisitorLayout) on folder Shared which is inside folder Views
7-create view model(its name is Article) to add and retrieve data to and from multiple tables
8-create folder Uploads to save uploaded images for articles on it
8-create 3 controllers in Controller folder , write functions and create Views
****Controllers and its functions****
-Home    --> functions {Login , Logout}
-Admin   --> functions {AddCategory , AddArticle(with assign category to each article) , GetArts(show created articles  with comments of 
each article and images if there are exist using functions GetArtComments and GetArtImgs) , EditArt(Update article) , 
DeleteArt(Remove Article) , DeleteImg(to remove image from gallery and replace it by another one in function EditArt)}
-Visitor --> functions {Register , GetArts(show created articles with comments and visitor can comment in any article 
and also can filter articles by categories)}
****Views(Pages) in folder Views****
-Home icludes one view --> Login
-Admin includes 4 views   --> AddArticle , AddCategory , EditArt , GetArts
-Visitor includes 2 Views --> Register , GetArts
9-make validation on model using DataAnnotations
10-use javascript to Filter Articles
