'use client'

import { Pagination } from "flowbite-react";

type Props = {
    currentPage: number,
    pageCount: number,
    pageChanged: (page: number) => void;
}

const customTheme = {
    pages: {   
        selector: {
            // Quick solution, issue is this is a client side component so accessing tailwind.config.ts is impossible
            // a good solution is to create a global css file with the colours defined in it, maybe in the future
            base:

                `
                w-8 sm:w-10 
                border
                border-third
                bg-second py-2 
                leading-tight
                text-third 
                enabled:hover:bg-secondary 
                enabled:hover:text-gray-700 
                `,
            active:
                `
                bg-third 
                text-secondary-600
                hover:bg-cyan-100 
                hover:text-cyan-700
                `
        },
        next: {
            base:
                `
                rounded-r-lg
                border
                border-secondary
                bg-third
                py-2
                px-2 sm:px-3
                leading-tight
                text-secondary-500 
                enabled:hover:bg-secondary
                enabled:hover:text-third-700
                `,
        },
        previous: {
            base:
                `
                ml-0 
                rounded-l-lg
                border
                border-secondary
                bg-third
                py-2
                px-2 sm:px-3
                leading-tight
                text-secondary-500 
                enabled:hover:bg-secondary
                enabled:hover:text-third-700
                `,

        }

    },
};

export default function AppPagination({ currentPage, pageCount, pageChanged }: Props) {
    return (
        <Pagination
            currentPage={currentPage}
            onPageChange={e => pageChanged(e)}
            totalPages={pageCount}
            layout="pagination"
            showIcons={true}
            theme={customTheme}
            className="mb-5"
        />
    )
}