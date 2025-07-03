import React from 'react';
import { useState } from "react";
import WeatherForecast from "../Components/WeatherForecast.tsx";
import LogoutLink from "../Components/LogoutLink.tsx";
import AuthorizeView, { AuthorizedUser } from "../Components/AuthorizeView.tsx";
import SearchForm from "../Components/DealSearch.tsx";


function Home() {
    const [deals, setDeals] = useState<any[]>([]);

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
        const data = await response.json();
        //console.log(data);
        //console.log(typeof data);
        //const arrayData = Array.isArray(data) ? data : Object.values(data);
        //setDeals(arrayData);
        //console.log(arrayData);
        //console.log(typeof arrayData);
        setDeals(Array.isArray(data) ? data : []);
    }

    function handleAdd(deal: any) {
        // Implement your add logic here
        console.log("Add clicked for:", deal);
    }

    return (
        <AuthorizeView>
            <span><LogoutLink>Logout <AuthorizedUser value="email" /></LogoutLink></span>
            <div className="min-h-screen flex flex-col items-center justify-center bg-gray-100">
                <h4>Search for PC games to add into your EasyDeal list</h4>
                <SearchForm onSearch={handleSearch}/>
                <ul className="list">
                    {deals.map((deal, idx) => (
                        <li key={idx} className="list-item">
                            <img className="deal-img" src={deal.thumb} />
                            <span>{deal.external ?? "Untitled Deal"}</span>
                            
                            <span> Cheapest deal: {deal.cheapest ?? "No deal found"}</span>
                            <span> Cheapest price: {deal.cheapestPrice ?? "No price found"}</span>
                            
                            <button
                                className="add ml-2 px-3 py-1 bg-green-600 text-white rounded hover:bg-green-700"
                                onClick={() => handleAdd(deal)}
                            >
                                Add
                            </button>
                        </li>
                    ))}
                </ul>
            </div>
            
        </AuthorizeView>
    );
}

export default Home;