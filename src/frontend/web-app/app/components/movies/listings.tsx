'use client'

import { Fragment, useEffect, useState } from "react";
import MovieCard from "./movie-card";
import AppPagination from "../shared/app-pagination";
import { Movie } from "@/types";
import { getData } from "@/app/actions/movie-actions";

export default function Listings() {
    const [movies, setMovies] = useState<Movie[]>([]);
    const [pageCount, setPageCount] = useState(0);
    const [pageNumber, setPageNumber] = useState(1);
    const url = "https://cdn.pixabay.com/photo/2023/11/09/19/36/zoo-8378189_1280.jpg";

    useEffect(() => {
        getData(pageNumber).then(data => {
            setMovies(data.items);
            setPageCount(data.totalCount);
        })
    }, [pageNumber])

    if (movies?.length === 0) return <h3>Loading...</h3>

    return (
        <Fragment>
            <div className="flex flex-grow justify-center p-6">
                <div className="grid grid-cols-2">
                    {movies?.map((movie, index) => (
                        <Fragment>
                            <MovieCard key={index} title="fff HEDAGHAEGHAEHAEHAEHAEHAEEA " description={movie.description} imageUrl={url} />
                            <MovieCard title="ffff" description={movie.description} imageUrl={url} />
                        </Fragment>
                    ))}
                </div>
            </div>
            <div className="flex justify-center b-2">
                <AppPagination currentPage={pageNumber} pageCount={pageCount} />
            </div>
        </Fragment>
    );
}