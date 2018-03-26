using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentHubApplication.API.Entities;
using StudentHubApplication.API.Helpers;
using StudentHubApplication.API.Models;

namespace StudentHubApplication.API.Services
{
    public class ApplicationInfoRepository : IApplicationInfoRepository
    {
        private ApplicationInfoContext _applicationInfoContext;
        private IPropertyMappingService _propertyMappingService;

        public ApplicationInfoRepository(ApplicationInfoContext applicationInfoContext, IPropertyMappingService propertyMappingService)
        {
            _applicationInfoContext = applicationInfoContext;
            _propertyMappingService = propertyMappingService;
        }

        #region Get Record(s)
        public Country GetCountry(int countryId)
        {
            return _applicationInfoContext.Countries.FirstOrDefault(c => c.Id == countryId);
        }

        //public IEnumerable<Application> GetApplications(ApplicationsResourceParameters applicationsResourceParameters)
        public PagedList<Application> GetApplications(ApplicationsResourceParameters applicationsResourceParameters)
        {
            // BEFORE APPLYING SORTING
            //var collectionBeforePaging = _applicationInfoContext.Applications
            //    .Include(a => a.CountryOfResidence)
            //    .Include(a => a.CountryOfBirth)
            //    .OrderBy(a => a.Id)
            //    .ThenBy(a => a.Gender)
            //    .AsQueryable();

            // Apply Sorting
            var collectionBeforePaging =
                _applicationInfoContext.Applications.ApplySort(applicationsResourceParameters.OrderBy,
                _propertyMappingService.GetPropertyMapping<ApplicationDto, Application>());

            // Filtering
            // http://localhost:63292/api/applications?gender=Female
            if (!string.IsNullOrEmpty(applicationsResourceParameters.Gender))
            {
                // trim & ignore casing
                var genreForWhereClause = applicationsResourceParameters.Gender
                    .Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Gender.ToLowerInvariant() == genreForWhereClause);
            }

            // Searching
            //http://localhost:63292/api/applications?searchQuery=vaibhav
            if (!string.IsNullOrEmpty(applicationsResourceParameters.SearchQuery))
            {
                // trim & ignore casing
                var searchQueryForWhereClause = applicationsResourceParameters.SearchQuery
                    .Trim().ToLowerInvariant();

                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Gender.ToLowerInvariant().Contains(searchQueryForWhereClause)
                    || a.FirstName.ToLowerInvariant().Contains(searchQueryForWhereClause)
                    || a.LastName.ToLowerInvariant().Contains(searchQueryForWhereClause));
            }

            // Adding Page Number and Page Size
            // http://localhost:63292/api/applications?pageNumber=2&pageSize=5
            return PagedList<Application>.Create(collectionBeforePaging, 
                applicationsResourceParameters.PageNumber,
                applicationsResourceParameters.PageSize);

            //return _applicationInfoContext.Applications
            //    .Include(a => a.CountryOfResidence)
            //    .Include(a => a.CountryOfBirth)
            //    .OrderBy(a => a.Id)
            //    //.Include(e => e.ApplicationQualifications)
            //    //.ThenInclude(q => q.Qualification)
            //    .ToList();
        }

        public Application GetApplication(int applicationId, bool includeQualification)
        {
            //    .FirstOrDefault(a => a.Id == applicationId);
            if (includeQualification)
            {

                return _applicationInfoContext.Applications
                    .Include(a => a.CountryOfResidence)
                    .Include(a => a.CountryOfBirth)
                    .Where(a => a.Id == applicationId)
                    .FirstOrDefault();
                //return _applicationInfoContext.Applications                     NOT USED, But should be used for   http://localhost:63292/api/applications/69?includeQualification=true
                //    .Include(c => c.Country)                                    NOT USED, But should be used for   http://localhost:63292/api/applications/69?includeQualification=true
                //    .Include(aq => aq.ApplicationQualifications)                NOT USED, But should be used for   http://localhost:63292/api/applications/69?includeQualification=true
                //    .ThenInclude(ca => ca.Qualification)                        NOT USED, But should be used for   http://localhost:63292/api/applications/69?includeQualification=true
                //    .Where(a => a.Id == applicationId)                          NOT USED, But should be used for   http://localhost:63292/api/applications/69?includeQualification=true
                //    .FirstOrDefault();                                          NOT USED, But should be used for   http://localhost:63292/api/applications/69?includeQualification=true
            }
            else
            {
                return _applicationInfoContext.Applications
                    .Include(a => a.CountryOfResidence)
                    .Include(a => a.CountryOfBirth)
                    .Where(a => a.Id == applicationId)
                    .FirstOrDefault();
            }
        }

        public Qualification GetQualification(int qualificationId)
        {
            return _applicationInfoContext.Qualifications.FirstOrDefault(q => q.Id == qualificationId);
        }
        
        public IEnumerable<ApplicationQualification> GetQualificationsForApplication(int applicationId)
        {
            return _applicationInfoContext.ApplicationQualifications
            //return _applicationInfoContext.Qualifications
                .Include(q => q.Qualification)
                //.Include(a => a.ApplicationQualifications)
                //.ThenInclude(q => q.Qualification)
                .Where(a => a.ApplicationId == applicationId)
                .ToList();
        }

        public ApplicationQualification GetApplicationQualificationForApplication(int applicationId, int id)
        {
            return _applicationInfoContext.ApplicationQualifications
                .Include(q => q.Qualification)
                .Where(aq => aq.ApplicationId == applicationId && aq.QualificationId == id)
                .FirstOrDefault();
        }

        public CourseCampus GetCourseCampus(int courseCampusId)
        {
            return _applicationInfoContext.CourseCampuses.FirstOrDefault(cc => cc.Id == courseCampusId);
        }

        public ApplicationCourseCampus GetCourseCampusForApplication(int applicationId, int id)
        {
            return _applicationInfoContext.ApplicationCourseCampuses
            //return _applicationInfoContext.Qualifications
                .Include(cc => cc.CourseCampus)
                .ThenInclude(ca => ca.Campus)
                .Include(cc => cc.CourseCampus)
                .ThenInclude(co => co.Course)
                .Where(a => a.ApplicationId == applicationId && a.Id == id)
                //.Where(a => a.ApplicationId == applicationId && a.CourseCampusId == id)
                .FirstOrDefault();
        }

        public ApplicationCourseCampus GetCourseCampusForApplicationByProvidingApplicationIdAndCourseCampusId(int applicationId, int courseCampusId)
        {
            return _applicationInfoContext.ApplicationCourseCampuses
                .Include(cc => cc.CourseCampus)
                .ThenInclude(ca => ca.Campus)
                .Include(cc => cc.CourseCampus)
                .ThenInclude(co => co.Course)
                .Where(a => a.ApplicationId == applicationId && a.CourseCampusId == courseCampusId)
                .FirstOrDefault();
        }

        public IEnumerable<ApplicationCourseCampus> GetCourseCampusesForApplication(int applicationId)
        {
            return _applicationInfoContext.ApplicationCourseCampuses
            //return _applicationInfoContext.Qualifications
                .Include(cc => cc.CourseCampus)
                .ThenInclude(ca => ca.Campus)
                .Include(cc => cc.CourseCampus)
                .ThenInclude(co => co.Course)
                .Where(a => a.ApplicationId == applicationId)
                .ToList();
        }

        #endregion

        #region If Record Exists
        public bool ApplicationExists(int applicationId)
        {
            return _applicationInfoContext.Applications.Any(a => a.Id == applicationId);
        }        

        public bool QualificationExists(int qualificationId)
        {
            return _applicationInfoContext.Qualifications.Any(a => a.Id == qualificationId);
        }

        public bool CourseCampusExists(int courseCampusId)
        {
            return _applicationInfoContext.CourseCampuses.Any(cc => cc.Id == courseCampusId);
        }
        public bool CourseCampusForApplicationExists(int applicationId, int id)
        {
            return _applicationInfoContext.ApplicationCourseCampuses.Any(acc => acc.ApplicationId == applicationId && acc.Id == id);
        }
        #endregion

        #region Add Record
        public void AddApplication(Application application)//, int countryId)
        {
            //var country = GetCountry(countryId);
            _applicationInfoContext.Applications.Add(application);
        }

        public void AddQualification(int applicationId, Qualification qualification)
        {
            var application = GetApplication(applicationId, false);

            qualification.ApplicationQualifications = new List<ApplicationQualification>
            {
                new ApplicationQualification{
                    Application = application,
                    Qualification = qualification
                }
            };
            _applicationInfoContext.Qualifications.Add(qualification);
        }

        public void AddApplicationQualifications(int applicationId, Qualification qualification)
        {
            var application = GetApplication(applicationId, false);

                var applicationQualification = new ApplicationQualification()
                {
                    Application = application,
                    Qualification = qualification
                };
                _applicationInfoContext.ApplicationQualifications.Add(applicationQualification);
        }

        public void AddCourseCampusForApplication(int applicationId, ApplicationCourseCampus courseCampusForApplication)
        {
            var application = GetApplication(applicationId, false);
            var courseCampus = GetCourseCampus(courseCampusForApplication.CourseCampusId);
            courseCampusForApplication.Application = application;
            courseCampusForApplication.CourseCampus = courseCampus;
            //var applicationCourseCampus = new ApplicationCourseCampus()   //  SHOULD BE USED THIS WAY
            //{                                                             //  SHOULD BE USED THIS WAY
            //    Application = application,                                //  SHOULD BE USED THIS WAY
            //    CourseCampus = courseCampus                               //  SHOULD BE USED THIS WAY
            //};                                                            //  SHOULD BE USED THIS WAY
            _applicationInfoContext.ApplicationCourseCampuses.Add(courseCampusForApplication);
        }
        #endregion

        #region Update Records
        public void UpdateCourseCampusForApplication(ApplicationCourseCampus applicationCourseCampus)
        {
            // no code in this implementation
        }
        #endregion

        #region Delete Records
        public void DeleteApplication(Application application)
        {
            _applicationInfoContext.Applications.Remove(application);
        }
        public void DeleteApplicationQualificationForApplication(ApplicationQualification applicationQualification)
        {
            _applicationInfoContext.ApplicationQualifications.Remove(applicationQualification);
        }
        public void DeleteApplicationCourseCampusForApplication(ApplicationCourseCampus applicationCourseCampus)
        {
            _applicationInfoContext.ApplicationCourseCampuses.Remove(applicationCourseCampus);
        }
        #endregion

        public bool Save()
        {
            return (_applicationInfoContext.SaveChanges() >= 0);
        }
        public async Task<bool> SaveAllAsync()
        {
            return (await _applicationInfoContext.SaveChangesAsync()) > 0;
        }

    }
}
