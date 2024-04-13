import { useParamsStore } from "@/hooks/useParamsStore";
import { TiSortAlphabetically } from "react-icons/ti";
import { BsFillStopCircleFill, BsGraphUp } from "react-icons/bs";
import { PiSortAscending } from "react-icons/pi";
import { PiSortDescending } from "react-icons/pi";
import { RiParentLine } from "react-icons/ri";

const pageSizeButtons = [12, 24, 48];

const sortButtons = [
    {
        label: 'Alphabetical',
        icon: TiSortAlphabetically,
        value: 'title'
    },
    {
        label: 'Rating',
        icon: RiParentLine ,
        value: 'rating'
    },
    {
        label: 'Release',
        icon: BsFillStopCircleFill,
        value: 'release'
    }
]

const orderButtons = [
    {
        label: 'Ascending',
        icon: PiSortAscending,
        value: 'asc'
    },
    {
        label: 'Descending',
        icon: PiSortDescending,
        value: 'desc'
    },
]

export default function Filters() {
    const pageSize = useParamsStore(state => state.pageSize);
    const orderBy = useParamsStore(state => state.orderBy);
    const sortOrder = useParamsStore(state => state.sortOrder);

    const setParams = useParamsStore(state => state.setParams);

    return (
        <div className="flex flex-col md:flex-row items-center">

            <div className="mb-4 md:mb-0 md:mr-4"> {/* "Order by" group */}
                <span className="mb-2 md:mb-0 md:mr-2 text-secondary font-bold">Order by:</span>
                <div className="flex">
                    {orderButtons.map(({ label, icon: Icon, value }, index) => (
                        <button
                            key={value}
                            onClick={() => setParams({ sortOrder: value })}
                            className={`
                                flex-shrink-0
                                p-2 sm:p-2 
                                text-center
                                border
                                border-third
                                bg-second 
                                py-1 
                                leading-tight
                                text-body 
                                hover:bg-secondary 
                                hover:text-gray-700 
                                ${sortOrder === value ? "bg-third text-body" : "bg-transparent text-third"}
                                ${index === 0 ? "rounded-l-2xl" : ""}
                                ${index === orderButtons.length - 1 ? "rounded-r-2xl" : ""}
                            `}
                        >
                            <div className="flex items-center justify-center">
                                <Icon className="mr-2" />
                                {label}
                            </div>
                        </button>
                    ))}
                </div>
            </div>

            <div className="mb-4 md:mb-0 mr-auto md:mr-4"> {/* "Sort by" group */}
                <span className="mb-2 md:mb-0 md:mr-2 text-secondary font-bold">Sort by:</span>
                <div className="flex">
                    {sortButtons.map(({ label, icon: Icon, value }, index) => (
                        <button
                            key={value}
                            onClick={() => setParams({ orderBy: value })}
                            className={`
                                flex-shrink-0
                                p-2 sm:p-2 
                                text-center
                                border
                                border-third
                                bg-second 
                                py-1 
                                leading-tight
                                text-body 
                                hover:bg-secondary 
                                hover:text-gray-700 
                                ${orderBy === value ? "bg-third text-body" : "bg-transparent text-third"}
                                ${index === 0 ? "rounded-l-2xl" : ""}
                                ${index === sortButtons.length - 1 ? "rounded-r-2xl" : ""}
                            `}
                        >
                            <div className="flex items-center justify-center">
                                <Icon className="mr-2" />
                                {label}
                            </div>
                        </button>
                    ))}
                </div>
            </div>

            <div> {/* "Page Size" group */}
                <span className="mb-2 md:mb-0 md:mr-2 text-secondary font-bold">Page Size:</span>
                <div className="flex">
                    {pageSizeButtons.map((size, index) => (
                        <button
                            key={size}
                            onClick={() => setParams({ pageSize: size })}
                            className={`
                                flex-shrink-0
                                p-2 sm:p-2 
                                text-center
                                border
                                border-third
                                bg-second 
                                py-1 
                                leading-tight
                                text-body 
                                hover:bg-secondary 
                                hover:text-gray-700 
                                ${pageSize === size ? "bg-third text-body" : "bg-transparent text-third"}
                                ${index === 0 ? "rounded-l-2xl" : ""}
                                ${index === pageSizeButtons.length - 1 ? "rounded-r-2xl" : ""}
                            `}
                        >
                            {size}
                        </button>
                    ))}
                </div>
            </div>
        </div>
    );
}