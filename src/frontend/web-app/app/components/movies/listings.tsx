'use client'

import { Fragment, useEffect, useState } from "react";
import MovieCard from "./movie-card";
import AppPagination from "../shared/app-pagination";
import { Movie, PagedResult } from "@/types";
import { getData } from "@/app/actions/movie-actions";
import Filters from "./filters";
import { useParamsStore } from "@/hooks/useParamsStore";
import { shallow } from "zustand/shallow";
import qs from 'query-string'
import EmptyFilter from "./empty-filter";

export default function Listings() {
    const [data, setData] = useState<PagedResult<Movie>>();

    const params = useParamsStore(state => ({
        pageNumber: state.pageNumber,
        pageSize: state.pageSize,
        searchTerm: state.searchTerm,
        sortColumn: state.orderBy,
        sortOrder: state.sortOrder
    }), shallow);
    const setParams = useParamsStore(state => state.setParams);

    const url = qs.stringifyUrl({ url: '', query: params })

    //const urlm = "https://cdn.pixabay.com/photo/2023/11/09/19/36/zoo-8378189_1280.jpg";

    function setPageNumber(pageNumber: number) {
        setParams({ pageNumber });
    }

    useEffect(() => {
        getData(url).then(data => {
            setData(data);
            console.log(data);
        })
    }, [url])

    if (!data) return <h3>Loading...</h3>

    return (
        <div className="relative">
            <div className="flex flex-grow justify-end mt-8 mx-auto max-w-full px-8 w-[900px] sm:px-8 items-center">
                <Filters />
            </div>
            {data.totalPages === 0 ? (
                <EmptyFilter />
            ) : (
                <>
                    <div className="flex flex-grow justify-center p-6 mx-auto max-w-full px-8 w-[900px] sm:px-8 ">
                        <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 gap-1">
                            {data.items?.map((movie, index) => (
                                <Fragment>
                                    <MovieCard isCarousel={false} movie={movie} key={index} />
                                </Fragment>
                            ))}
                        </div>
                    </div>

                    <div className="flex justify-center p-6">
                        <AppPagination currentPage={params.pageNumber} pageCount={data.totalPages || 0} pageChanged={setPageNumber} />
                    </div>
                </>
            )
            }

        </div>
    );
}