import WeatherForecast from "../Components/WeatherForecast.tsx";
import LogoutLink from "../Components/LogoutLink.tsx";
import AuthorizeView, { AuthorizedUser } from "../Components/AuthorizeView.tsx";
import SearchForm from "../Components/DealSearch.tsx";

function Home() {

    async function handleSearch(query: string) {
        console.log("Searching for:", query);
        // Implement your search logic here
        const response = await fetch('dealsearch', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ query })
        });
        console.log(response);
    };

    return (
        <AuthorizeView>
            <span><LogoutLink>Logout <AuthorizedUser value="email" /></LogoutLink></span>
            <div className="min-h-screen flex items-center justify-center bg-gray-100">
                <h4> Search for PC games to add into your EasyDeal list</h4>
                <SearchForm onSearch={handleSearch} />
            </div>
            <WeatherForecast />
        </AuthorizeView>
    );
}

export default Home;