'use client'

import { useParamsStore } from "@/hooks/useParamsStore";
import { useRouter } from "next/navigation";
import { Fragment } from "react";
import { FaSearch } from "react-icons/fa";

export default function Search() {
    const setParams = useParamsStore(state => state.setParams);
    const setSearchValue = useParamsStore(state => state.setSearchValue)
    const router = useRouter();

    const searchValue = useParamsStore(state => state.searchValue);

    function onChange(event: any) {
        setSearchValue(event.target.value);
    }

    function search() {
        setParams({ searchTerm: searchValue });
        // over here redirect the mto the search page
        router.push('/search')
    }

    return (
        <Fragment>
            <div className="flex-grow mx-4 flex items-center justify-center">
                <input
                    onKeyDown={(e: any) => {
                        if (e.key === 'Enter') search();
                    }}
                    onChange={onChange}
                    value={searchValue}
                    type="text"
                    placeholder="Search for movies..."
                    className="
                        bg-transparent
                        w-full
                        md:max-w-xs
                        lg:max-w-md
                        p-2
                        rounded-lg
                        outline-none
                        focus:outline-none
                        text-primary
                        border-secondary
                        border-b-2
                        focus:border-third
                        focus:ring-0"
                />
                <button onClick={search} className="bg-header text-secondary p-2 rounded-md ml-2">
                    <FaSearch />
                </button>
            </div>
        </Fragment>
    )
}
