using Gameplay.Services;
using Gameplay.UI.View;

namespace Gameplay.UI.Presenter
{
    public class LocationPresenter : IHomeGameplayListener, IBattleGameplayListener
    {
        private readonly Location _homeLocation;
        private readonly Location _battleLocation;

        public Location HomeLocation => _battleLocation;
        public Location BattleLocation => _battleLocation;

        public LocationPresenter(Location homeLocation, Location battleLocation)
        {
            _homeLocation = homeLocation;
            _battleLocation = battleLocation;
        }

        public void OnHomeGameplay()
        {
            _battleLocation.Hide();
            _homeLocation.Show();
        }

        public void OnBattleGameplay()
        {
            _homeLocation.Hide();
            _battleLocation.Show();
        }
    }
}