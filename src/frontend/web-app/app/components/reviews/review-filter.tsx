import React, { ChangeEvent } from 'react'

interface ReviewFilterProps {
    onFilterChange: (rating: number | null) => void;
    onSortChange: (orderBy: 'newest' | 'oldest' | 'none') => void;
}

const options = [
    { value: 'all', label: 'All' },
    { value: '1', label: '1 Star' },
    { value: '2', label: '2 Stars' },
    { value: '3', label: '3 Stars' },
    { value: '4', label: '4 Stars' },
    { value: '5', label: '5 Stars' },
];

function ReviewFilters({ onFilterChange, onSortChange }: ReviewFilterProps) {
    const handleFilterChange = (event: ChangeEvent<HTMLSelectElement>) => {
        const rating = event.target.value ? parseInt(event.target.value) : null;
        onFilterChange(rating);
    };

    return (

        <div className="flex justify-between items-center">

            <div className="mr-2">
                <label htmlFor='time-filter' className='text-secondary font-semibold'>Filter by date:</label>
                <select id='time-filter' onChange={e => onSortChange(e.target.value as 'newest' | 'oldest' | 'none')}
                    className="rounded-full py-2 px-4 border-2 border-third  bg-secondary bg-opacity-75 font-semibold text-darkHeader focus:border-third focus:ring-third focus:outline-none">
                    <option value="none">No sort</option>
                    <option value="newest">Newest first</option>
                    <option value="oldest">Oldest first</option>
                </select>
            </div>

            <div>
                <label htmlFor="rating-filter" className='text-secondary font-semibold'>Filter by stars:</label>
                <select id="rating-filter" onChange={handleFilterChange}
                    className="rounded-full py-2 px-4 border-2 border-third bg-secondary bg-opacity-75 font-semibold text-darkHeader focus:border-third focus:ring-third focus:outline-none">
                    <option value="0">All</option>
                    <option value="1">1 Star</option>
                    <option value="2">2 Stars</option>
                    <option value="3">3 Stars</option>
                    <option value="4">4 Stars</option>
                    <option value="5">5 Stars</option>
                </select>
            </div>
        </div>

    );
}

export default ReviewFilters;
