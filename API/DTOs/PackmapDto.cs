using System;

namespace API.DTOs
{
    public class PackmapDto
    {
        public long Id { get; set; }
        public string Customer { get; set; }
        public string ArticleName { get; set; }
        public string Mapdata { get; set; }
        public int maptypeid { get; set; }
        public int ModuleId { get; set; }
        public int LocationId { get; set; }

    }
}