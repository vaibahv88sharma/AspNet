using StudentHubApplication.API.Models;
using StudentHubApplication.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentHubApplication.API.Helpers;

namespace StudentHubApplication.API.Services
{
    public interface IApplicationInfoRepository
    {
        bool ApplicationExists(int applicationId);
        bool QualificationExists(int qualificationId);
        bool CourseCampusExists(int courseCampusId);
        bool CourseCampusForApplicationExists(int applicationId, int id);
        Country GetCountry(int countryId);
        PagedList<Application> GetApplications(ApplicationsResourceParameters applicationsResourceParameters);
        //IEnumerable<Application> GetApplications(ApplicationsResourceParameters applicationsResourceParameters);
        Application GetApplication(int applicationId, bool includeQualification);
        Qualification GetQualification(int qualificationId);
        ApplicationQualification GetApplicationQualificationForApplication(int applicationId, int id);
        CourseCampus GetCourseCampus(int courseCampusId);
        IEnumerable<ApplicationQualification> GetQualificationsForApplication(int applicationId);
        ApplicationCourseCampus GetCourseCampusForApplication(int applicationId, int id);
        IEnumerable<ApplicationCourseCampus> GetCourseCampusesForApplication(int applicationId);
        void AddApplication(Application application);
        void AddQualification(int applicationId, Qualification qualification);
        void AddApplicationQualifications(int applicationId, Qualification qualifications);
        void AddCourseCampusForApplication(int applicationId, ApplicationCourseCampus courseCampusForApplication);
        void UpdateCourseCampusForApplication(ApplicationCourseCampus applicationCourseCampus);
        void DeleteApplication(Application application);
        void DeleteApplicationQualificationForApplication(ApplicationQualification applicationQualification);
        void DeleteApplicationCourseCampusForApplication(ApplicationCourseCampus applicationCourseCampus);
        bool Save();
    }
}
