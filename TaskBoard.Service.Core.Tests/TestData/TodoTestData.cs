using TaskBoard.Service.Core.Domain.Dtos;
using TaskBoard.Service.Core.Domain.Entities;

namespace TaskBoard.Service.Core.Tests.TestData;

public static class TodoTestData
{
    #region Owners
    public static User USER_1 => new ()
    {
        Id = Guid.NewGuid().ToString()
    };
    public static User USER_2 => new ()
    {
        Id = Guid.NewGuid().ToString()
    };
    #endregion

    #region Todos
    public static Todo TODO_1 => new ()
    {
        Id = 1,
        Title = "Check the plants!",
        Description = "Give some water to the plants",
        IsCompleted = false,
        DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(15)),
        Owner = USER_1
    };

    public static Todo TODO_2 => new ()
    {
        Id = 2,
        Title = "Check the water!",
        Description = "Put some water in the Tank",
        IsCompleted = false,
        DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(15)),
        Owner = USER_1
    };

    public static Todo TODO_3 => new ()
    {
        Id = 3,
        Title = "Feed the dog",
        Description = "Give some food to the dog",
        IsCompleted = true,
        DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(15)),
        Owner = USER_2
    };

    public static Todo TODO_4 => new ()
    {
        Id = 4,
        Title = "Check dog food",
        Description = "If there is no enough food we have to buy some dog food",
        IsCompleted = false,
        DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(15)),
        Owner = USER_2
    };

    public static Todo TODO_NOT_COMPLETED => TODO_1;

    public static Todo TODO_COMPLETED => TODO_3;
    #endregion

    #region TodoInputDtos
    public static TodoInputDto TODO_1_INPUT => new ()
    {
        Title = "Check the plants!",
        Description = "Give some water to the plants",
        IsCompleted = false,
        DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(15))
    };

    public static TodoInputDto TODO_2_INPUT => new ()
    {
        Title = "Check the water!",
        Description = "Put some water in the Tank",
        IsCompleted = false,
        DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(15))
    };

    public static TodoInputDto TODO_3_INPUT => new ()
    {
        Title = "Feed the dog",
        Description = "Give some food to the dog",
        IsCompleted = false,
        DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(15))
    };

    public static TodoInputDto TODO_4_INPUT => new ()
    {
        Title = "Check dog food",
        Description = "If there is no enough food we have to buy some dog food",
        IsCompleted = false,
        DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(15))
    };
    #endregion

    #region TodoOutputDtos
    public static TodoOutputDto TODO_1_OUTPUT => new ()
    {
        Id = 1,
        Title = "Check the plants!",
        Description = "Give some water to the plants",
        IsCompleted = true,
        DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(15))
    };

    public static TodoOutputDto TODO_2_OUTPUT => new()
    {
        Id = 2,
        Title = "Check the water!",
        Description = "Put some water in the Tank",
        IsCompleted = false,
        DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(15)),
    };

    public static TodoOutputDto TODO_OUTPUT_COMPLETED => TODO_1_OUTPUT;
    #endregion
}
