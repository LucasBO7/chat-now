import { IoIosSearch } from "react-icons/io";


export const TextInput = ({ styles, value, onChange, placeholder }) => {
    return (
        <div className={`flex items-center justify-between w-full h-12 rounded-[14px] bg-white ${styles}`}>
            <input
                className={`w-[80%] h-full ml-4 border-none bg-transparent placeholder-gray-600 text-black font-NotoThai text-sm`}
                type="text"
                value={value}
                onChange={onChange}
                placeholder={placeholder}
            />
            <IoIosSearch className="w-[15%] h-full p-2" />
        </div>
    );
}