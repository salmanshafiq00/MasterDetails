//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using System.Threading;
//using System;
//using TestConfiguration.Models;

//namespace UEMS.Application.Features
//{
//    public class UpdateTestConfigCommand : IRequest<Result<int>>
//    {
//        public int Id { get; set; }
//        public int AdmissionReferenceId { get; set; }
//        public int BatchId { get; set; }
//        public int SessionId { get; set; }
//        public decimal MinimumGPA { get; set; }
//        public decimal PassMark { get; set; }
//        public List<TestConfigDetail> TestConfigDetails { get; set; } = new List<TestConfigDetail>();


//        public class UpdateTestConfigCommandHandler : IRequestHandler<UpdateTestConfigCommand, Result<int>>
//        {
//            private readonly ITestConfigRepository _repository;
//            private readonly ITestConfigDetailRepository _detailRepository;
//            private readonly IMapper _mapper;
//            private IUnitOfWork _unitOfWork { get; set; }
//            public UpdateTestConfigCommandHandler(ITestConfigRepository repository, ITestConfigDetailRepository detailRepository, IUnitOfWork unitOfWork, IMapper mapper)
//            {
//                _repository = repository;
//                _detailRepository = detailRepository;
//                _mapper = mapper;
//                _unitOfWork = unitOfWork;
//            }
//            public async Task<Result<int>> Handle(UpdateTestConfigCommand request, CancellationToken cancellationToken)
//            {
//                try
//                {
//                    var testConfig = await _repository.GetByIdAsync(request.Id);

//                    if (testConfig == null)
//                    {
//                        return Result<int>.Fail($"TestConfig Not Found");
//                    }
//                    else
//                    {
//                        testConfig.AdmissionReferenceId = request.AdmissionReferenceId;
//                        testConfig.BatchId = request.BatchId;
//                        testConfig.SessionId = request.SessionId;
//                        testConfig.MinimumGPA = request.MinimumGPA;
//                        testConfig.PassMark = request.PassMark;

//                        var testConfigDetails = await _detailRepository.GetAllAsync();
//                        for (int i = 0; i < request.TestConfigDetails.Count; i++)
//                        {
//                            if (request.TestConfigDetails[i].Id > 0 && request.TestConfigDetails[i].SubjectId < 1)
//                            {
//                                if (testConfigDetails.Any(tcd => tcd.Id == request.TestConfigDetails[i].Id))
//                                {
//                                    await _detailRepository.DeleteAsync(await _detailRepository.GetByIdAsync(request.TestConfigDetails[i].Id));
//                                    request.TestConfigDetails.Remove(request.TestConfigDetails[i]);
//                                    i--;
//                                }

//                            }
//                        }

//                        testConfig.TestConfigDetails = request.TestConfigDetails;
//                        await _repository.UpdateAsync(testConfig);
//                        await _unitOfWork.Commit(cancellationToken);
//                        return Result<int>.Success(testConfig.Id);
//                    }
//                }
//                catch (Exception ex)
//                {

//                    return Result<int>.Fail(ex.Message);
//                }
//            }
//        }
//    }
//}


////////////////////// Create command//////////////////

//using AspNetCoreHero.Results;
//using AutoMapper;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using UEMS.Application.Interfaces.Repositories.Academic;
//using UEMS.Application.Interfaces.Repositories;
//using UEMS.Application.Interfaces.Repositories.Admission;
//using UEMS.Domain.Entities.Academic;
//using UEMS.Domain.Entities.Admission;
//using UEMS.Application.Features.Admission.TestConfigDetail.Queries;

//namespace UEMS.Application.Features
//{
//    public class CreateTestConfigCommand : IRequest<Result<int>>
//    {
//        public int AdmissionReferenceId { get; set; }
//        public int BatchId { get; set; }
//        public int SessionId { get; set; }
//        public decimal MinimumGPA { get; set; }
//        public decimal PassMark { get; set; }
//        public List<TestConfigDetail> TestConfigDetails { get; set; } = new List<TestConfigDetail>();
//    }
//    public class CreateTestConfigCommandHandler : IRequestHandler<CreateTestConfigCommand, Result<int>>
//    {
//        private readonly ITestConfigRepository _repository;
//        private readonly IMapper _mapper;
//        private IUnitOfWork _unitOfWork { get; set; }
//        public CreateTestConfigCommandHandler(ITestConfigRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
//        {
//            _repository = repository;
//            _mapper = mapper;
//            _unitOfWork = unitOfWork;
//        }
//        public async Task<Result<int>> Handle(CreateTestConfigCommand request, CancellationToken cancellationToken)
//        {
//            try
//            {

//                var entity = _mapper.Map<TestConfig>(request);

//                await _repository.AddAsync(entity);
//                await _unitOfWork.Commit(cancellationToken);
//                return Result<int>.Success(entity.Id);


//            }
//            catch (Exception ex)
//            {

//                return Result<int>.Fail(ex.Message);
//            }
//        }
//    }

//}


///////////////////////////Delete Command///////////////////////////


//using AspNetCoreHero.Results;
//using AutoMapper;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UEMS.Application.Features.Academic.Lookup.Commands;
//using UEMS.Application.Interfaces.Repositories.Admission;
//using UEMS.Application.Interfaces.Repositories;
//using System.Threading;
//using UEMS.Application.Constants;
//using UEMS.Application.Interfaces.Repositories.Academic;

//namespace UEMS.Application.Features.Admission.TestConfig.Commands
//{
//    public class DeleteTestConfigCommand : IRequest<Result<int>>
//    {
//        public int Id { get; set; }
//        public class DeleteTestConfigCommandHandler : IRequestHandler<DeleteTestConfigCommand, Result<int>>
//        {
//            private readonly ITestConfigRepository _repository;
//            private readonly IMapper _mapper;
//            private IUnitOfWork _unitOfWork { get; set; }
//            public DeleteTestConfigCommandHandler(ITestConfigRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
//            {
//                _repository = repository;
//                _mapper = mapper;
//                _unitOfWork = unitOfWork;
//            }

//            public async Task<Result<int>> Handle(DeleteTestConfigCommand request, CancellationToken cancellationToken)
//            {
//                try
//                {

//                    var entity = await _repository.GetByIdAsync(request.Id);
//                    if (entity == null)
//                    {
//                        return Result<int>.Fail("Lookup not Exists.");
//                    }
//                    else
//                    {
//                        await _repository.DeleteAsync(entity);
//                        await _unitOfWork.Commit(cancellationToken);
//                        return Result<int>.Success(LocalizerConstant.DELETE);
//                    }

//                }
//                catch (Exception ex)
//                {

//                    return Result<int>.Fail(ex.Message);
//                }
//            }
//        }
//    }
//}
//}
