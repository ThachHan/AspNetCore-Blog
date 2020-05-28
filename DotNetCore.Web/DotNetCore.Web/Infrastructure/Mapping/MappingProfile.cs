using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.Data.Entity;
using DotNetCore.Web.Models;
using DotNetCore.Web.Areas.Admin.Models;


namespace DotNetCore.Web.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Content, Models.ContentViewModel>();
            CreateMap<Models.ContentViewModel, Content>();
            CreateMap<Content, Areas.Admin.Models.ContentViewModel>();
            CreateMap<Areas.Admin.Models.ContentViewModel, Content>();
            CreateMap<Content,Models.GalaryViewModel>();
            CreateMap<Models.GalaryViewModel, Content>();
            CreateMap<Content, Areas.Admin.Models.PublishingContentViewModel>();
            CreateMap<Areas.Admin.Models.PublishingContentViewModel, Content>();

            CreateMap<Category, Models.CategoryViewModel>();
            CreateMap<Models.CategoryViewModel, Category>();
            CreateMap<Category, Areas.Admin.Models.CategoryViewModel>();
            CreateMap<Areas.Admin.Models.CategoryViewModel, Category>();

            CreateMap<Category, Models.CategorySimpleViewModel>();

            CreateMap<SystemParameter, Models.SystemParameterViewModel>();
            CreateMap<Models.SystemParameterViewModel, SystemParameter>();
            CreateMap<SystemParameter, Areas.Admin.Models.SystemParameterViewModel>();
            CreateMap<Areas.Admin.Models.SystemParameterViewModel, SystemParameter>();

            CreateMap<Advertisement, Models.AdvertisementViewModel>();
            CreateMap<Models.AdvertisementViewModel, Advertisement>();
            CreateMap<Advertisement, Areas.Admin.Models.AdvertisementViewModel>();
            CreateMap<Areas.Admin.Models.AdvertisementViewModel, Advertisement>();


            CreateMap<Author, Models.AuthorViewModel>();
            CreateMap<Models.AuthorViewModel, Author>();
            CreateMap<Author, Areas.Admin.Models.AuthorViewModel>();
            CreateMap<Areas.Admin.Models.AuthorViewModel, Author>();

            CreateMap<Tag, Models.TagViewModel>();
            CreateMap<Models.TagViewModel, Tag>();
            CreateMap<Tag, Areas.Admin.Models.TagViewModel>();
            CreateMap<Areas.Admin.Models.TagViewModel, Tag>();

            CreateMap<Comment, Models.CommentViewModel>();
            CreateMap<Models.CommentViewModel, Comment>();
            CreateMap<Comment, Models.UserCommentViewModel>();
            CreateMap<Models.UserCommentViewModel, Comment>();
            CreateMap<Comment, Models.PostCommentViewModel>();
            CreateMap<Models.PostCommentViewModel, Comment>();
            CreateMap<Comment, Areas.Admin.Models.CommentViewModel>();
            CreateMap<Areas.Admin.Models.CommentViewModel, Comment>();

            CreateMap<Account, Areas.Identity.Models.RegisterViewModel>();
            CreateMap<Areas.Identity.Models.RegisterViewModel, Account>();
            CreateMap<Account, Areas.Admin.Models.AccountDetailViewModel>();
            CreateMap<Areas.Admin.Models.AccountDetailViewModel, Account>();
            CreateMap<Account, Areas.Admin.Models.EditAccountViewModel>();
            CreateMap<Areas.Admin.Models.EditAccountViewModel, Account>();
            CreateMap<Account, Models.AccountViewModel>();
            CreateMap<Models.AccountViewModel, Account>();
            CreateMap<Areas.Admin.Data.ApplicationUser, Models.AccountViewModel>();
            CreateMap<Models.AccountViewModel, Areas.Admin.Data.ApplicationUser>();


        }
    }
}
