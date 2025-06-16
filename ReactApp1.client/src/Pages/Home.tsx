import WeatherForecast from "../Components/WeatherForecast.tsx";
import LogoutLink from "../Components/LogoutLink.tsx";
import AuthorizeView, { AuthorizedUser } from "../Components/AuthorizeView.tsx";
import SearchForm from "../Components/DealSearch.tsx";

function Home() {
    return (
        <AuthorizeView>
            <span><LogoutLink>Logout <AuthorizedUser value="email" /></LogoutLink></span>
            <div className="min-h-screen flex items-center justify-center bg-gray-100">
                <p> Search for a PC game to show possible matches to add into your wishlist</p>
                <SearchForm />
            </div>
            <WeatherForecast />
        </AuthorizeView>
    );
}

export default Home;