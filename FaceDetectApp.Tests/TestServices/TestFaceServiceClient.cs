using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FaceDetectApp.Tests.TestServices
{
    internal sealed class TestFaceServiceClient : IFaceServiceClient
    {
        public Task<Face[]> DetectAsync(Stream imageStream, bool returnFaceId = true, 
            bool returnFaceLandmarks = false, IEnumerable<FaceAttributeType> returnFaceAttributes = null)
        {
            return Task.FromResult(new Face[]
            {
                new Face
                {
                    FaceAttributes = new FaceAttributes
                    {
                        Age = 20,
                        Gender = "male",
                        Glasses = Glasses.Sunglasses
                    },
                    FaceRectangle = new FaceRectangle
                    {
                        Height = 100,
                        Width = 100,
                        Left = 150,
                        Top = 200
                    }
                }
            });
        }

        #region NotNeeded
        public HttpRequestHeaders DefaultRequestHeaders
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Task<AddPersistedFaceResult> AddFaceToFaceListAsync(string faceListId, Stream imageStream, string userData = null, FaceRectangle targetFace = null)
        {
            throw new NotImplementedException();
        }

        public Task<AddPersistedFaceResult> AddFaceToFaceListAsync(string faceListId, string imageUrl, string userData = null, FaceRectangle targetFace = null)
        {
            throw new NotImplementedException();
        }

        public Task<AddPersistedFaceResult> AddPersonFaceAsync(string personGroupId, Guid personId, Stream imageStream, string userData = null, FaceRectangle targetFace = null)
        {
            throw new NotImplementedException();
        }

        public Task<AddPersistedFaceResult> AddPersonFaceAsync(string personGroupId, Guid personId, string imageUrl, string userData = null, FaceRectangle targetFace = null)
        {
            throw new NotImplementedException();
        }

        public Task CreateFaceListAsync(string faceListId, string name, string userData)
        {
            throw new NotImplementedException();
        }

        public Task<CreatePersonResult> CreatePersonAsync(string personGroupId, string name, string userData = null)
        {
            throw new NotImplementedException();
        }

        public Task CreatePersonGroupAsync(string personGroupId, string name, string userData = null)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFaceFromFaceListAsync(string faceListId, Guid persistedFaceId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFaceListAsync(string faceListId)
        {
            throw new NotImplementedException();
        }

        public Task DeletePersonAsync(string personGroupId, Guid personId)
        {
            throw new NotImplementedException();
        }

        public Task DeletePersonFaceAsync(string personGroupId, Guid personId, Guid persistedFaceId)
        {
            throw new NotImplementedException();
        }

        public Task DeletePersonGroupAsync(string personGroupId)
        {
            throw new NotImplementedException();
        }

        public Task<Face[]> DetectAsync(string imageUrl, bool returnFaceId = true, bool returnFaceLandmarks = false, IEnumerable<FaceAttributeType> returnFaceAttributes = null)
        {
            throw new NotImplementedException();
        }

        public Task<SimilarPersistedFace[]> FindSimilarAsync(Guid faceId, string faceListId, int maxNumOfCandidatesReturned = 20)
        {
            throw new NotImplementedException();
        }

        public Task<SimilarFace[]> FindSimilarAsync(Guid faceId, Guid[] faceIds, int maxNumOfCandidatesReturned = 20)
        {
            throw new NotImplementedException();
        }

        public Task<FaceList> GetFaceListAsync(string faceListId)
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetPersonAsync(string personGroupId, Guid personId)
        {
            throw new NotImplementedException();
        }

        public Task<PersonFace> GetPersonFaceAsync(string personGroupId, Guid personId, Guid persistedFaceId)
        {
            throw new NotImplementedException();
        }

        public Task<PersonGroup> GetPersonGroupAsync(string personGroupId)
        {
            throw new NotImplementedException();
        }

        public Task<PersonGroup[]> GetPersonGroupsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TrainingStatus> GetPersonGroupTrainingStatusAsync(string personGroupId)
        {
            throw new NotImplementedException();
        }

        public Task<Person[]> GetPersonsAsync(string personGroupId)
        {
            throw new NotImplementedException();
        }

        public Task<GroupResult> GroupAsync(Guid[] faceIds)
        {
            throw new NotImplementedException();
        }

        public Task<IdentifyResult[]> IdentifyAsync(string personGroupId, Guid[] faceIds, int maxNumOfCandidatesReturned = 1)
        {
            throw new NotImplementedException();
        }

        public Task<FaceListMetadata[]> ListFaceListsAsync()
        {
            throw new NotImplementedException();
        }

        public Task TrainPersonGroupAsync(string personGroupId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateFaceListAsync(string faceListId, string name, string userData)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePersonAsync(string personGroupId, Guid personId, string name, string userData = null)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePersonFaceAsync(string personGroupId, Guid personId, Guid persistedFaceId, string userData = null)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePersonGroupAsync(string personGroupId, string name, string userData = null)
        {
            throw new NotImplementedException();
        }

        public Task<VerifyResult> VerifyAsync(Guid faceId1, Guid faceId2)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
