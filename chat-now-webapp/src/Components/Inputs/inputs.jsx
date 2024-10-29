export const TextInput = ({ styles, value, onChange, placeholder, icon }) => {
    return (
        <div className={`flex items-center justify-between w-full h-12 rounded-[14px] bg-white ${styles}`}>
            <input
                className={`w-[100%] h-full ml-4 border-none bg-transparent placeholder-gray-600 text-black font-NotoThai text-sm`}
                type="text"
                value={value}
                onChange={onChange}
                placeholder={placeholder}
            />
            {icon}
        </div>
    );
}