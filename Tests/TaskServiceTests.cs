namespace Tests;

public class TaskServiceTests
{
    public ITasksService GetTasksService()
    {
        var logger = Mock.Of<ILogger<TasksService>>();

        Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
        unitOfWork.Setup(_ =>
            _.TasksRepository.CreateAsync(It.IsAny<TaskEntity>()))
            .Returns<TaskEntity>(arg => Task.FromResult(arg));

        unitOfWork.Setup(_ =>
            _.TasksRepository.UpdateAsync(It.IsAny<TaskEntity>()))
            .Returns<TaskEntity>(arg => Task.FromResult(arg));

        var validationService = new ValidationService();

        var myProfile = new MapperProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        IMapper mapper = new Mapper(configuration);

       return new TasksService(logger, unitOfWork.Object, validationService, mapper);
    }

    [Fact]
    public async void Create_Task_ReturnsCreatedTask()
    {
        //Arrange
        var tasksService = GetTasksService();
        var taskDTO = new TaskDTO()
        {
            Name = "Test",
            PriorityValue = 1,
            StatusText = StatusesEnum.NotStarted.GetName(),
        };


        //Act
        var task = await tasksService.CreateAsync(taskDTO);

        //Assert
        Assert.NotNull(task);
        Assert.Equal(taskDTO.Name, task.Name);
    }
}