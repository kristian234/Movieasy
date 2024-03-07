import { useParamsStore } from "@/hooks/useParamsStore";
import { RxValue } from "react-icons/rx";

const pageSizeButtons = [12, 24, 48];

export default function Filters() {
    const pageSize = useParamsStore(state => state.pageSize);
    const setParams = useParamsStore(state => state.setParams);

    return (
        <div className="flex flex-col items-center md:flex-row md:items-center">
            <span className="mb-2 md:mb-0 md:mr-2 text-secondary visible smm:hidden font-bold">Page Size:</span>
            <div className="lex rounded-2xl overflow-hidder">
                {pageSizeButtons.map((size, index) => (
                    <button
                        key={size}
                        onClick={() => setParams({pageSize: size})}
                        className={`
                          flex-1
                          p-2 sm:p-2 
                          text-center
                          border
                          border-third
                          bg-second py-2 
                          leading-tight
                          text-secondary 
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
    );
}
