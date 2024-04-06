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
    const [data, setData] = useState<PagedResult<Movie> | null>(null);

    const params = useParamsStore(
        (state) => ({
            pageNumber: state.pageNumber,
            pageSize: state.pageSize,
            searchTerm: state.searchTerm,
            sortColumn: state.orderBy,
            sortOrder: state.sortOrder,
        }),
        shallow
    );
    const setParams = useParamsStore((state) => state.setParams);

    const url = qs.stringifyUrl({ url: "", query: params });

    function setPageNumber(pageNumber: number) {
        setParams({ pageNumber });
    }

    useEffect(() => {
        getData(url)
            .then((data) => {
                setData(data);
                console.log(data);
            })
            .catch((error) => {
                console.error("Error fetching data:", error);
                setData(null);
            });
    }, [url]);

    if (!data) {
        return (
            <div className="flex items-center justify-center h-screen">
                <h3>Loading...</h3>
            </div>
        );
    }

    return (
        <div className="relative">
            <div className="flex flex-grow justify-center mt-8 mx-auto max-w-full px-8 w-[900px] sm:px-8 items-center">
                <Filters />
            </div>
            {data.totalPages === 0 ? (
                <EmptyFilter />
            ) : (
                <>
                    <div className="flex flex-grow justify-center p-6 mx-auto max-w-full px-8 w-[900px] sm:px-8 ">
                        <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 gap-1">
                            {data.items?.map((movie, index) => (
                                <Fragment key={index}>
                                    <MovieCard isCarousel={false} movie={movie} />
                                </Fragment>
                            ))}
                        </div>
                    </div>

                    <div className="flex justify-center p-6">
                        <AppPagination
                            currentPage={params.pageNumber}
                            pageCount={data.totalPages || 0}
                            pageChanged={setPageNumber}
                        />
                    </div>
                </>
            )}
        </div>
    );
}
