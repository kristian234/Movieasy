import { useParamsStore } from "@/hooks/useParamsStore";
import { AiOutlineSortAscending } from "react-icons/ai";
import { BsFillStopCircleFill, BsGraphUp } from "react-icons/bs";

const pageSizeButtons = [12, 24, 48];

const orderButtons = [
    {
        label: 'Alphabetical',
        icon: AiOutlineSortAscending,
        value: 'title'
    },
    {
        label: 'Rating',
        icon: BsGraphUp,
        value: 'rating'
    },
    {
        label: 'Recently added',
        icon: BsFillStopCircleFill,
        value: 'recently'
    }
]

export default function Filters() {
    const pageSize = useParamsStore(state => state.pageSize);
    const orderBy = useParamsStore(state => state.orderBy);

    const setParams = useParamsStore(state => state.setParams);

    return (
        <div className="flex flex-row items-center md:flex-row md:items-center">
            <div className="mr-4">
                <span className="mb-2 md:mb-0 md:mr-2 text-secondary visible smm:hidden font-bold">Order by:</span>
                <div className="lex rounded-2xl overflow-hidder">
                    {orderButtons.map(({ label, icon: Icon, value }, index) => (
                        <button
                            key={value}
                            onClick={() => setParams({ orderBy: value })}
                            className={`
                                flex-1
                                p-2 sm:p-2 
                                text-center
                                border
                                border-third
                                bg-second py-1 
                                leading-tight
                                text-body 
                                enabled:hover:bg-secondary 
                                enabled:hover:text-gray-700 
                                ${orderBy === value ?
                                    "bg-third text-body" : "bg-transparent text-third"
                                }
                                ${index === 0 ? "rounded-l-2xl" : ""}
                                ${index === pageSizeButtons.length - 1 ? "rounded-r-2xl" : ""}
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

            <div>
                <span className="mb-2 md:mb-0 md:mr-2 text-secondary visible smm:hidden font-bold">Page Size:</span>
                <div className="lex rounded-2xl overflow-hidder">
                    {pageSizeButtons.map((size, index) => (
                        <button
                            key={size}
                            onClick={() => setParams({ pageSize: size })}
                            className={`
                                flex-1
                                p-2 sm:p-2 
                                text-center
                                border
                                border-third
                                bg-second py-1 
                                leading-tight
                                text-body 
                                enabled:hover:bg-secondary 
                                enabled:hover:text-gray-700 
                                ${pageSize === size ?
                                    "bg-third text-body" : "bg-transparent text-third"
                                }
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
