import { Spinner } from "flowbite-react";

export default function LoadingFallback() {
    return (
      <div className="flex justify-center items-center h-screen">
        <Spinner />
      </div>
    );
  }