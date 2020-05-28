Callie Blog

## Website

 https://sd2632.azurewebsites.net/

```bash
Account Admin : Admin / P@ssword1234

Account Visitor : kcllubu / P@ssword1234
```

## Function

```python

    - Login by Authorize (custom Identity, not use Default UI of authorize .NET Core).
	
	- Admin : + Manage Category : Insert, Update , Detail. (Category of website. User can manage category (show top/ show bot/ not show))
			  + Manage Content : Insert, Update , Detail.
			  ( Content want to show for website must be published. Status : Writed -> Approved -> Published. Content can be published for one or another category but can be have more tags. If content is not enough good, user can reject and update it before published)
			  + Manage Advertisement : Insert, Update , Detail. (One advertisement only show one position : Top , Center , Bottom (can show/hide follow status))
			  + Manage Author : Insert, Update , Detail. (Author of content)
			  + Manage Tag : Insert, Update. (Tag of content.)
			  + Manage Account : Insert, Update, Detail. (Create User for 3 role : Administrator, Approver , Writer)
			  + Manage System Parameter : Insert, Update (SiteLogo, Description, Social Link, Social Follower, Copy Right, Email, Phone, Address,...)
			  
			  (Almost all category, content, tag, author have friendly url (slug))
			  
	- User  : Can register/login and comment posts.

```

## Technial
- Authorization.
- ViewComponent
- Middleware
- DI
- GenericRepository
- AutoMapper

