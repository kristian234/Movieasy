'use client'

import { Fragment } from "react";
import MovieCard from "./movie-card";

export default function Listings() {
    const movies = [
        { title: 'Movie 1', description: 'Description 1', imageUrl: 'https://cdn.pixabay.com/photo/2023/04/24/19/08/pirate-7948887_960_720.jpg' },
        { title: 'Movie 2', description: 'Description 2', imageUrl: 'https://cdn.pixabay.com/photo/2023/04/24/19/08/pirate-7948887_960_720.jpg' },
        { title: 'Movie 3', description: 'Description 3', imageUrl: 'https://cdn.pixabay.com/photo/2023/04/24/19/08/pirate-7948887_960_720.jpg' },
        { title: 'Movie 3', description: 'Description 3', imageUrl: 'https://cdn.pixabay.com/photo/2023/04/24/19/08/pirate-7948887_960_720.jpg' },
        { title: 'Movie 3', description: 'Description 3', imageUrl: 'https://cdn.pixabay.com/photo/2023/04/24/19/08/pirate-7948887_960_720.jpg' },
        { title: 'Movie 3', description: 'Description 3', imageUrl: 'https://cdn.pixabay.com/photo/2023/04/24/19/08/pirate-7948887_960_720.jpg' },
        { title: 'Movie 3', description: 'Description 3', imageUrl: 'https://cdn.pixabay.com/photo/2023/04/24/19/08/pirate-7948887_960_720.jpg' },
        { title: 'Movie 3', description: 'Description 3', imageUrl: 'https://cdn.pixabay.com/photo/2023/04/24/19/08/pirate-7948887_960_720.jpg' },
        { title: 'Movie 3', description: 'Description 3', imageUrl: 'https://cdn.pixabay.com/photo/2023/04/24/19/08/pirate-7948887_960_720.jpg' },
        { title: 'Movie 3', description: 'Description 3', imageUrl: 'https://cdn.pixabay.com/photo/2023/04/24/19/08/pirate-7948887_960_720.jpg' },
        { title: 'Movie 3', description: 'Description 3', imageUrl: 'https://cdn.pixabay.com/photo/2023/04/24/19/08/pirate-7948887_960_720.jpg' },
        { title: 'Movie 3', description: 'Description 3', imageUrl: 'https://cdn.pixabay.com/photo/2023/04/24/19/08/pirate-7948887_960_720.jpg' },
        { title: 'Movie 3', description: 'Description 3', imageUrl: 'https://cdn.pixabay.com/photo/2023/04/24/19/08/pirate-7948887_960_720.jpg' },
        { title: 'Movie 3', description: 'Description 3', imageUrl: 'https://cdn.pixabay.com/photo/2023/04/24/19/08/pirate-7948887_960_720.jpg' },
        { title: 'Movie 3', description: 'Description 3', imageUrl: 'https://cdn.pixabay.com/photo/2023/04/24/19/08/pirate-7948887_960_720.jpg' },
        { title: 'Movie 3', description: 'Description 3', imageUrl: 'https://cdn.pixabay.com/photo/2023/04/24/19/08/pirate-7948887_960_720.jpg' },

        // Add more movies as needed
    ];

    return (
        <div className="flex flex-wrap justify-center p-6">
            <div className="grid grid-cols-2 gap-6">
                {movies.map((movie, index) => (
                    <MovieCard key={index} title={movie.title} description={movie.description} imageUrl={movie.imageUrl} />
                ))}
            </div>
        </div>
    );
}