namespace TeachingBACKEND.Domain.DTOs
{
    // ─── Admin input DTOs (POST / PUT) ───────────────────────────────────────────

    public class DragSpellInputDTO
    {
        public string Word { get; set; }
        public List<string> Letters { get; set; }
        public string Hint { get; set; }
        public string? ImageFileId { get; set; }
    }

    public class DragOrderTileInputDTO
    {
        public string Text { get; set; }
    }

    public class DragOrderInputDTO
    {
        public List<DragOrderTileInputDTO> Tiles { get; set; }
        public List<int> CorrectOrder { get; set; } // 0-based indices into Tiles
    }

    public class DragMatchPairInputDTO
    {
        public string Word { get; set; }
        public string? ImageFileId { get; set; }
    }

    public class DragMatchInputDTO
    {
        public List<DragMatchPairInputDTO> Pairs { get; set; }
    }

    // ─── Shared tile DTO (used in both admin and student responses) ──────────────

    public class DragOrderTileDTO
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }

    // ─── Admin response DTOs (GET single quiz) ───────────────────────────────────

    public class DragSpellAdminDTO
    {
        public string Word { get; set; }
        public List<string> Letters { get; set; }
        public string Hint { get; set; }
        public string? ImageFileId { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class DragOrderAdminDTO
    {
        public List<DragOrderTileDTO> Tiles { get; set; }
        public List<string> CorrectOrder { get; set; }
    }

    public class DragMatchPairAdminDTO
    {
        public string Id { get; set; }
        public string Word { get; set; }
        public string? ImageFileId { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class DragMatchAdminDTO
    {
        public List<DragMatchPairAdminDTO> Pairs { get; set; }
    }

    // ─── Student response DTOs (GET single quiz) ─────────────────────────────────

    public class DragSpellStudentDTO
    {
        public int WordLength { get; set; }
        public List<string> Letters { get; set; }
        public string Hint { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class DragOrderStudentDTO
    {
        public List<DragOrderTileDTO> Tiles { get; set; }
    }

    public class DragMatchWordDTO
    {
        public string WordId { get; set; }
        public string Text { get; set; }
    }

    public class DragMatchImageDTO
    {
        public string ImageId { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class DragMatchStudentDTO
    {
        public List<DragMatchWordDTO> Words { get; set; }
        public List<DragMatchImageDTO> Images { get; set; }
    }

    // ─── Student submission DTO ───────────────────────────────────────────────────

    public class DragMatchSubmissionDTO
    {
        public string WordId { get; set; }
        public string ImageId { get; set; }
    }
}
