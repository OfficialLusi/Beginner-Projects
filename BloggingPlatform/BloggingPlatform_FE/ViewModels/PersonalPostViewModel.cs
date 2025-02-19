using BloggingPlatform_FE.Interfaces;
using BloggingPlatform_FE.Models;
using LusiUtilsLibrary.Backend.APIs_REST;
using LusiUtilsLibrary.Backend.Initialization;
using LusiUtilsLibrary.Frontend.MVVMHelpers;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace BloggingPlatform_FE.ViewModels;

public class PersonalPostViewModel : INotifyPropertyChanged
{
    private readonly IRequestService_FE _requestService;
    private readonly INavigationService _navigationService;
    private readonly IMemoryService _memoryService;
    private readonly ILogger<PersonalPostViewModel> _logger;

    private string _searchedWord;

    public PersonalPostViewModel(IRequestService_FE requestService, INavigationService navigationService, IMemoryService memoryService, ILogger<PersonalPostViewModel> logger)
    {
        InitializeChecks.InitialCheck(requestService, "Request Service cannot be null");
        InitializeChecks.InitialCheck(navigationService, "Navigation Service cannot be null");
        InitializeChecks.InitialCheck(memoryService, "Memory Service cannot be null");
        InitializeChecks.InitialCheck(logger, "Logger cannot be null");

        _requestService = requestService;
        _navigationService = navigationService;
        _memoryService = memoryService;
        _logger = logger;

        HomeButton = new RelayCommand(async () => await GoToHome());
        WritePostButton = new RelayCommand(async () => await GoToWritePost());
        SeeMyPostButton = new RelayCommand(async () => await GoToSeeMyPosts());
        ExitButton = new RelayCommand(async () => await Exit());
        SearchButton = new RelayCommand(async () => await Search());

        ShowMyAllPosts();
    }

    public string SearchedWord
    {
        get => _searchedWord;
        set
        {
            _searchedWord = value;
            OnPropertyChanged(nameof(SearchedWord));
        }
    }

    public ICommand HomeButton { get; }
    public ICommand WritePostButton { get; }
    public ICommand SeeMyPostButton { get; }
    public ICommand ExitButton { get; }
    public ICommand SearchButton { get; }

    public ObservableCollection<BlogPostDto> BlogPosts { get; set; } = new ObservableCollection<BlogPostDto>();


    private async Task GoToHome() => _navigationService.NavigateTo("Home");

    private async Task GoToWritePost() => _navigationService.NavigateTo("WritePost");

    private async Task GoToSeeMyPosts() => _navigationService.NavigateTo("PersonalPosts");

    private async Task Exit() => App.Current.Shutdown();

    private async Task Search()
    {
        if (_searchedWord == string.Empty)
        {
            await ShowMyAllPosts();
            return;
        }

        BlogPosts.Clear();

        int currentUserId = _memoryService.GetCurrentUser().UserId;

        ApiResponse<List<BlogPostDto>> data = await _requestService.GetAllBlogPosts();

        List<BlogPostDto> userPosts = data.Data.Where(x => x.UserId == currentUserId).ToList();

        foreach (BlogPostDto blogPostDto in userPosts)
        {
            if (blogPostDto.PostTags.Contains(_searchedWord, StringComparison.InvariantCultureIgnoreCase) || blogPostDto.PostTitle.Contains(_searchedWord, StringComparison.InvariantCultureIgnoreCase))
                BlogPosts.Add(blogPostDto);
        }
    }

    private async Task ShowMyAllPosts()
    {

        int currentUserId = _memoryService.GetCurrentUser().UserId;
        BlogPosts.Clear();

        ApiResponse<List<BlogPostDto>> data = await _requestService.GetAllBlogPosts();

        if (data.StatusCode == System.Net.HttpStatusCode.OK && data.Data != null)
        {
            _logger.LogInformation("HomeViewModel - Correctly received list of all blog posts");
            foreach (var post in data.Data)
            {
                if(post.UserId == currentUserId)
                    BlogPosts.Add(post);
            }
        }
    }

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion
}