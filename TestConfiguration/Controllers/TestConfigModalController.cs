using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestConfiguration.Controllers
{
    public class TestConfigModalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //  [Authorize(Policy = Permissions.AdmitCardEnables.View)]
        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetAllTestConfigQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<TestConfigViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        //  [Authorize(Policy = Permissions.ApplicationFormConfigs.View)]
        public async Task<JsonResult> OnGetCreateOrEdit(int id = 0)
        {
            if (id == 0)
            {
                var testConfigViewModel = new TestConfigViewModel();
                testConfigViewModel.TestConfigDetails.Add(new TestConfigDetailViewModel());
                testConfigViewModel.TestConfigDetails.Add(new TestConfigDetailViewModel());
                testConfigViewModel.TestConfigDetails.Add(new TestConfigDetailViewModel());
                testConfigViewModel.TestConfigDetails.Add(new TestConfigDetailViewModel());
                testConfigViewModel.TestConfigDetails.Add(new TestConfigDetailViewModel());
                testConfigViewModel.TestConfigDetails.Add(new TestConfigDetailViewModel());

                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", testConfigViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GetTestConfigByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var testConfigViewModel = _mapper.Map<TestConfigViewModel>(response.Data);
                    var childNumber = testConfigViewModel.TestConfigDetails.Count;
                    var iterationNumber = 6 - childNumber;
                    for (int i = 0; i < iterationNumber; i++)
                    {
                        testConfigViewModel.TestConfigDetails.Add(new TestConfigDetailViewModel() { TestConfigId = testConfigViewModel.Id });
                    }
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", testConfigViewModel) });
                }
                return null;
            }
        }

        // [Authorize(Policy = Permissions.AdmissionTest.View)]
        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit([FromBody] TestConfigViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (model.Id == 0)
                {
                    model.TestConfigDetails.RemoveAll(tcd => tcd.SubjectId < 1 && tcd.ResultGradeId < 1);

                    var mappedData = _mapper.Map<CreateTestConfigCommand>(model);
                    var result = await _mediator.Send(mappedData);
                    if (result.Succeeded)
                    {
                        model.Id = result.Data;
                        _notify.Success(_localizer[LocalizerConstant.SAVED]);
                        //return new JsonResult(new { isValid = true });
                    }
                    else
                    {
                        _notify.Error(_localizer[result.Message]);
                        return new JsonResult(new { isValid = false });
                    }
                }
                else
                {
                    model.TestConfigDetails.RemoveAll(tcd => tcd.TestConfigId > 0 && tcd.SubjectId < 1 && tcd.ResultGradeId < 1);
                    var mappedData = _mapper.Map<UpdateTestConfigCommand>(model);
                    var result = await _mediator.Send(mappedData);
                    if (result.Succeeded)
                    {
                        _notify.Information(_localizer[LocalizerConstant.UPDATE_MSG]);
                        //return new JsonResult(new { isValid = true });
                    }
                    else
                    {
                        _notify.Error(_localizer[result.Message]);
                        return new JsonResult(new { isValid = false });
                    }
                }
                var response = await _mediator.Send(new GetAllTestConfigQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<TestConfigViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                    return new JsonResult(new { isValid = true, html = html });
                }
                else
                {
                    _notify.Error(response.Message);
                    return new JsonResult(new { isValid = false });
                }
            }
            else
            {
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", model);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        // [Authorize(Policy = Permissions.AdmitCardEnables.Delete)]
        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int id)
        {
            var deleteCommand = await _mediator.Send(new DeleteTestConfigCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information(_localizer[LocalizerConstant.DELETE]);
                var response = await _mediator.Send(new GetAllTestConfigQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<TestConfigViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                    return new JsonResult(new { isValid = true, html = html });
                }
                else
                {
                    _notify.Error(_localizer[response.Message]);
                    return new JsonResult(new { isValid = false });
                }
            }
            else
            {
                _notify.Error(_localizer[deleteCommand.Message]);
                return new JsonResult(new { isValid = false });
            }
        }

        public async Task<List<GetAllResultGradeResponse>> GetAllResultGrade()
        {

            List<GetAllResultGradeResponse> list = new List<GetAllResultGradeResponse>();
            var response = await _mediator.Send(new GetAllResultGradeQuery());
            if (response.Succeeded)
            {
                list = _mapper.Map<List<GetAllResultGradeResponse>>(response.Data);
                return list;
            }
            return null;
        }

        public async Task<List<GetAllSubjectResponse>> GetAllSubject()
        {
            List<GetAllSubjectResponse> list = new List<GetAllSubjectResponse>();
            var response = await _mediator.Send(new GetAllSubjectQuery());
            if (response.Succeeded)
            {
                list = _mapper.Map<List<GetAllSubjectResponse>>(response.Data);
                return list;
            }
            return null;
        }
    }
}



