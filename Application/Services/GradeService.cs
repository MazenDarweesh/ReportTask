﻿using Domain.DTOs;
using Infrastructure;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Application.Services
{
    public class GradeService : IGradeService
    {
        private readonly AppDbContext _context;

        public GradeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<GradeDTO>> GetGradesAsync()
        {
            return await _context.Grades
                .Select(g => new GradeDTO
                {
                    Id = g.Id.ToString(),
                    Name = g.Name
                })
                .ToListAsync();
        }
    }
}
