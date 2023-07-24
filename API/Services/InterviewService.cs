using API.Contracts;
using API.DataTransferObjects.Interviews;

namespace API.Services;

public class InterviewService
{
    private readonly IInterviewRepository _interviewRepository;

    public InterviewService(IInterviewRepository interviewRepository)
    {
        _interviewRepository = interviewRepository;
    }

    public IEnumerable<InterviewDtoGet> Get()
    {
        var interviews = _interviewRepository.GetAll().ToList();
        if (!interviews.Any()) return Enumerable.Empty<InterviewDtoGet>();
        List<InterviewDtoGet> interviewDtoGets = new();

        foreach (var interview in interviews)
        {
            interviewDtoGets.Add((InterviewDtoGet)interview);
        }

        return interviewDtoGets;
    }

    public InterviewDtoGet? Get(Guid guid)
    {
        var interview = _interviewRepository.GetByGuid(guid);
        if (interview is null) return null!;

        return (InterviewDtoGet)interview;
    }

    public InterviewDtoCreate? Create(InterviewDtoCreate interviewDtoCreate)
    {
        var interviewCreated = _interviewRepository.Create(interviewDtoCreate);
        if (interviewCreated is null) return null!;

        return (InterviewDtoCreate)interviewCreated;
    }

    public int Update(InterviewDtoUpdate interviewDtoUpdate)
    {
        var interview = _interviewRepository.GetByGuid(interviewDtoUpdate.Guid);
        if (interview is null) return -1;

        var interviewUpdated = _interviewRepository.Update(interviewDtoUpdate);
        return !interviewUpdated ? 0 : 1;
    }

    public int Delete(Guid guid)
    {
        var interview = _interviewRepository.GetByGuid(guid);
        if (interview is null) return -1;

        var interviewDeleted = _interviewRepository.Delete(interview);
        return !interviewDeleted ? 0 : 1;
    }
}
