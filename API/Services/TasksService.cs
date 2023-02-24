using API.Common;
using API.DTOs;
using API.Exceptions;
using API.Extensions;
using API.Interfaces;
using API.Persistence.Entities;
using AutoMapper;

namespace API.Services;

public class TasksService : ITasksService
{
    private readonly ILogger<TasksService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidationService _validationService;
    private readonly IMapper _mapper;

    public TasksService(ILogger<TasksService> logger, IUnitOfWork unitOfWork, IValidationService validationService, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validationService = validationService;
        _mapper = mapper;
    }

    public async Task<TaskDTO> CreateAsync(TaskDTO taskDTO)
    {
        try
        {
            var validationResult = await _validationService.Validate(taskDTO);
            
            if (!validationResult.IsValid)
            {
                throw new InvalidOperationException(string.Join(", ", validationResult.Errors));
            }

            var isExistByName = await _unitOfWork.TasksRepository.IsExistByNameAsync(taskDTO.Name);

            if (isExistByName)
            {
                throw new TaskEntityAlreadyExistException($"Task with provided name '{taskDTO.Name}' are already exist. Name must be unique.");
            }

            taskDTO.Status = (int?)EnumHelper.CastToStatusesEnum(taskDTO.StatusText);
            
            var task = _mapper.Map<TaskEntity>(taskDTO);

            var result = await _unitOfWork.TasksRepository.CreateAsync(task);

            return _mapper.Map<TaskDTO>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred in {nameof(CreateAsync)} method");
            throw;
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        try
        {
            var item = await _unitOfWork.TasksRepository.GetByIdAsync(id);
            
            if (item == null)
            {
                throw new TaskEntityNotFoundException($"Task with provided id '{id}' does not exist.");
            }

            if (item.Status == null || item.Status.Value != (int)StatusesEnum.Completed)
            {
                throw new InvalidOperationException("You can remove task only in 'completed' status");
            }
                
            await _unitOfWork.TasksRepository.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred in {nameof(DeleteAsync)} method");
            throw;
        }
    }

    public async Task<TaskDTO> GetTaskAsync(Guid id)
    {
        try
        {
            var item = await _unitOfWork.TasksRepository.GetByIdAsync(id);
            return _mapper.Map<TaskDTO>(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred in {nameof(GetTaskAsync)} method");
            throw;
        }
    }

    public async Task<IEnumerable<TaskDTO>> GetTasksAsync()
    {
        try
        {
            var items = await _unitOfWork.TasksRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TaskDTO>>(items);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred in {nameof(GetTasksAsync)} method");
            throw;
        }
    }

    public async Task<TaskDTO> UpdateAsync(TaskDTO taskDTO)
    {
        try
        {
            var validationResult = await _validationService.Validate(taskDTO);

            if (!validationResult.IsValid)
            {
                throw new InvalidOperationException(string.Join(", ", validationResult.Errors));
            }

            taskDTO.Status = (int?)EnumHelper.CastToStatusesEnum(taskDTO.StatusText);
            
            var item = _mapper.Map<TaskEntity>(taskDTO);
            
            var result = await _unitOfWork.TasksRepository.UpdateAsync(item);
            return _mapper.Map<TaskDTO>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred in {nameof(UpdateAsync)} method");
            throw;
        }
    }

    public async Task<IEnumerable<StatusDTO>> GetStatusListAsync()
    {
        try
        {
            var result = new List<StatusDTO>();
            var values = Enum.GetValues(typeof(StatusesEnum)).Cast<StatusesEnum>();
            foreach (var item in values)
            {
                result.Add(new StatusDTO { StatusNumber = (int)item, StatusText = item.GetName()});
            }

            return await Task.FromResult(result);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, $"An error occurred in {nameof(GetStatusListAsync)} method");
            throw;
        }
    }
}
