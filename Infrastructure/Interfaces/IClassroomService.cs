﻿using Domain.DTOs;



namespace Infrastructure.Interfaces
{
    public interface IClassroomService
    {
        Task<List<ClassroomDTO>> GetClassroomsAsync();
    }
}
