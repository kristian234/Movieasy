'use client'

import { Fragment } from "react";
import { FaSearch } from "react-icons/fa";

export default function Search() {
    return (
        <Fragment>
            <div className="flex-grow mx-4 flex items-center justify-center">
                <input
                    type="text"
                    placeholder="Search movies..."
                    className="
                        bg-transparent
                        w-full
                        md:max-w-xs
                        lg:max-w-md
                        p-2
                        rounded-lg
                        outline-none
                        focus:outline-none
                        border-secondary
                        border-b-2
                        focus:border-transparent
                        focus:ring-0"
                />
                <button className="bg-header text-secondary p-2 rounded-md ml-2">
                    <FaSearch />
                </button>
            </div>
        </Fragment>
    )
}
