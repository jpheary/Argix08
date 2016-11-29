using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.Profile;
using System.Collections.Generic;
using System.Web.Configuration;

//
public class MembershipServices {
    //Members
    private string mUsername="";
    
    public const string GUESTROLE = "guests";
    public const string ADMINROLE = "administrators";
    public const string FILECLAIMSROLE = "FileClaimsMember";
    public const string PODIMAGEROLE = "PODImageMember";
    public const string POSEARCHROLE = "pomembers";
    public const string REPORTSROLE = "rsmembers";
    public const string TRACKINGROLE = "members";
    public const string TRACKINGWSROLE = "wsmembers";

    //Interface
    public MembershipServices() {
        //Constructor for the current logged-in member
        if(Membership.GetUser() == null) throw new ApplicationException("There is no current member logged-in.");
        this.mUsername = Membership.GetUser().UserName;
    }
    public MembershipServices(string username) { 
        //Constructor for any member
        if(username.Trim().Length == 0) throw new ApplicationException("Username (" + username + ") is invalid.");
        if(Membership.FindUsersByName(username).Count == 0) throw new ApplicationException("Username (" + username + ") is not a member.");
        this.mUsername = username;
    }
    public string Username { get { return this.mUsername; } }
    public MembershipUser Member { get { return Membership.GetUser(this.mUsername); } }
    public ProfileCommon MemberProfile {
        get {
            ProfileCommon profile = new ProfileCommon().GetProfile(this.mUsername);
            if(IsAdmin || IsArgix) profile.Type = "";
            return profile;
        }
    }
    public bool IsArgix {
        get {
            ProfileCommon profile = GetMemberProfile(this.mUsername);
            return (profile.ClientVendorID == TrackingServices.ID_ARGIX);
        }
    }
    public bool IsAdmin { get { return Roles.IsUserInRole(this.mUsername,ADMINROLE); } }
    public bool IsFileClaims { get { return Roles.IsUserInRole(this.mUsername,FILECLAIMSROLE); } }
    public bool IsPODMember { get { return Roles.IsUserInRole(this.mUsername,PODIMAGEROLE); } }
    public bool IsPOMember { get { return Roles.IsUserInRole(this.mUsername,POSEARCHROLE); } }
    public bool IsRSMember { get { return Roles.IsUserInRole(this.mUsername,REPORTSROLE); } }
    public bool IsPasswordExpired {
        get {
            MembershipUser user = GetMember(this.mUsername);
            ProfileCommon profile = GetMemberProfile(this.mUsername);
            int expireDays = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["PasswordExpiration"]);
            TimeSpan ts = DateTime.Today.Subtract(user.LastPasswordChangedDate);
            if(ts.Days > expireDays || profile.PasswordReset)
                return true;
            else
                return false;
        }
    }
    
    public MembershipDS GetTrackingUsers() {
        //
        ProfileCommon profileCommon = new ProfileCommon();
        ProfileCommon profile = null;
        string email = "";
        string[] users = Roles.GetUsersInRole(TRACKINGROLE);
        MembershipDS member = new MembershipDS();

        //Append members
        for(int i=0; i<users.Length; i++) {
            email = Membership.GetUser(users[i]).Email;
            profile = profileCommon.GetProfile(users[i]);
            if(profile.Type.Length == 0) profile.Type = "client";
            if(profile.ClientVendorID.Length == 0) profile.ClientVendorID = TrackingServices.ID_ARGIX;
            member.MemberTable.AddMemberTableRow(profile.UserName,profile.UserFullName,email,profile.Company,profile.Type,profile.ClientVendorID,profile.WebServiceUser,profile.LastActivityDate,profile.LastUpdatedDate);
        }
        member.AcceptChanges();
        return member;
    }
    public MembersDS GetMembers() {
        //Load a list of Member selections
        MembersDS ds = new MembersDS();
        MembershipUserCollection members = Membership.GetAllUsers();
        foreach(MembershipUser member in members) {
            MembersDS.MembershipTableRow row = ds.MembershipTable.NewMembershipTableRow();
            row.Comment = member.Comment;
            row.CreateDate = member.CreationDate;
            row.Email = member.Email;
            row.IsApproved = member.IsApproved;
            row.IsLockedOut = member.IsLockedOut;
            row.IsOnline = member.IsOnline;
            row.LastActivityDate = member.LastActivityDate;
            row.LastLockoutDate = member.LastLockoutDate;
            row.LastLoginDate = member.LastLoginDate;
            row.LastPasswordChangedDate = member.LastPasswordChangedDate;
            row.PasswordQuestion = member.PasswordQuestion;
            row.UserName = member.UserName;

            ProfileCommon profile = new ProfileCommon().GetProfile(member.UserName);
            row.UserFullName = profile.UserFullName;
            row.Company = profile.Company;
            ds.MembershipTable.AddMembershipTableRow(row);
        }
        ds.AcceptChanges();
        return ds;
    }
    public MembershipUser GetMember(string username) { return Membership.GetUser(username); }
    public ProfileCommon GetMemberProfile(string username) {
        //
        ProfileCommon profile = new ProfileCommon().GetProfile(username);
        if(Roles.IsUserInRole(username,ADMINROLE) || profile.ClientVendorID == TrackingServices.ID_ARGIX) profile.Type = "";
        return profile;
    }
    public void UpdateUser(string userID,string userFullName,string email,string company,string companyID) {
        MembershipUser user = Membership.GetUser(userID);
        user.Email = email;
        Membership.UpdateUser(user);

        ProfileCommon profile = new ProfileCommon().GetProfile(userID);
        if(profile.Type.Length == 0) profile.Type = "client";
        profile.UserFullName = userFullName;
        profile.Company = company;
        profile.ClientVendorID = companyID;
        profile.Save();
    }
    public bool DeleteUser(string userID) {
        return Membership.DeleteUser(userID,true);
    }

}

