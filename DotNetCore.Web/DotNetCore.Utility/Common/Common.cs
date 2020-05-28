using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DotNetCore.Utility
{
    public class Common
    {
        public enum ContentStatus
        {
            Writed = 1,
            Approved = 2,
            Published = 3
        }
        public enum RoutingType
        {
            Category = 1,
            Content = 2,
            Author = 3, 
            Tag = 4
        }

        public enum SocialPosition
        {
            Navigation = 1,
            Body = 2,
            Footer = 3
        }
        public enum NewsletterPosition
        {
            Body = 1,
            Footer = 2
        }
        public enum ListCategoryPosition
        {
            Body = 1,
            Footer = 2
        }
        public enum TagPosition
        {
            Body = 1,
            Footer = 2
        }
        public enum NavigationPosition
        {
            Main = 1,
            Aside = 2,
            Footer = 3
        }
        public enum ContentPosition
        {
            Top = 1,
            Bottom = 2
        }
        public enum SystemParameterType
        {
            [Description("Site Title")]
            SiteTitle = 1,
            [Description("Site Logo Url")]
            SiteLogoUrl = 2,
            [Description("Site Logo Footer Url")]
            SiteLogoFooterUrl = 3,
            [Description("Site Description")]
            SiteDescription = 4,
            [Description("Homepage Keywords")]
            HomepageKeywords = 5,
            [Description("Homepage Description")]
            HomepageDescription = 6,
            [Description("Google Analytics Key")]
            GoogleAnalyticsKey = 7,
            [Description("Facebook Link")]
            FacebookLink = 8,
            [Description("Facebook Followers")]
            FacebookFollowers = 9,
            [Description("Twitter Link")]
            TwitterLink = 10,
            [Description("Twitter Followers")]
            TwitterFollowers = 11,
            [Description("Google Link")]
            GoogleLink = 12,
            [Description("Google Followers")]
            GoogleFollowers = 13,
            [Description("Instagram Link")]
            InstagramLink = 14,
            [Description("Instagram Followers")]
            InstagramFollowers = 15,
            [Description("Phone")]
            Phone = 16,
            [Description("Fax")]
            Fax = 17,
            [Description("Website")]
            Website = 18,
            [Description("Email")]
            Email = 19,
            [Description("Address")]
            Address = 20,
            [Description("GoogleMap")]
            GoogleMap = 21,
            [Description("CopyRight")]
            CopyRight = 22,
        }
        public enum AdvertisementPosition
        {
            BodyTop = 1,
            BodyCenter = 2,
            BodyBottom = 3
        }

        public enum AccountRole
        {
            Administrator = 1,
            Approver = 2,
            Writer = 3,
            User = 4
        }

        public enum UserPosition
        {
            AsideNavigation = 1,
            MainHeader = 2
        }
    }
    public class Constants
    {
        public static string AccountAdmin = "Admin";
        public static string AccountLogin = "UserLogin";
        public static string CategoryController = "Category";
        public static string ContentController = "Content";
        public static string CategoryAction = "LoadCategoryAsync";
        public static string ContentAction = "DetailAsync";
        public static string AuthorController = "Author";
        public static string TagController = "Tag";
        public static string AuthorAction = "LoadAuthorAsync";
        public static string TagAction = "LoadTag";
        public static string TruncateContent = "-vv";
        public static string UnknowErrorMessage = "Unknow Error";
        public static string ErrorDeleteAccount = "Can not delete account";
        public static string CategoryVisible = "Visible";
        public static string CategoryInvisible = "Invisible";
        public static string InputSpecialCharacter = "Not allow input special character.";
        public static string InvalidLoginErrorMsg = "Username or password is incorrect. Please login again.";
        public static string InvalidOldPassword = "Password does not match. Please input again.";
        public static string UpdateSuccessfully = "Updated Successfully!";
        public static string InsertSuccessfully = "Inserted Successfully!";
        public static string DefaultPassword = "P@ssword1234";
    }
}
