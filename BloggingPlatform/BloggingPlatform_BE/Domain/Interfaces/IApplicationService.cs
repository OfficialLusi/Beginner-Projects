using BloggingPlatform_BE.Application.DTOs;

namespace BloggingPlatform_BE.Domain.Interfaces
{
    public interface IApplicationService
    {
        public void AddUser(UserDto user);
        public void UpdateUser(UserDto user);
        public void DeleteUser(Guid userGuid);
        public UserDto GetUserByGuid(Guid userGuid);
        public List<UserDto> GetAllUsers();

        public void AddBlogPost(BlogPostDto blogPost);
        public void UpdateBlogPost(BlogPostDto blogPost);
        public void DeleteBlogPost(Guid blogPostGuid);
        public BlogPostDto GetBlogPostByGuid(Guid blogPostGuid);
        public List<BlogPostDto> GetAllBlogPosts();

        public void CreateDataBase();
    }
}
