# Tên dự án: FashionShop

# 1. Mô tả:
- Đây là dự án môn học: Đồ án công nghệ thông tin
- Web FashionShop là một trang web bán quần áo online. Hệ thống được chia làm ba phần: quản trị viên, nhân viên và khách hàng. Về FrontEnd sử dụng giao diện có sẳn và có thay đổi, xây dựng một số giao diện để phù hợp với dự án, về phía BackEnd xây dựng API bằng ASP.NET Core và lưu trữ dữ liệu ở SQL Server 
  
- FrontEnd: **HTML, CSS, Javascript, AngularJS, Bootstrap 3, JQuery, Ajax**
- BackEnd: **ASP.NET Core**
- Cơ sở dữ liệu: **SQL Server**

# 2. Sơ đồ thực thể liên kết: 
![db_fashionshop](https://github.com/lequanghao2002/DoAnCNTT/assets/113456985/d67f9f9b-d9dc-4573-bd6e-8387e1c86889)

# 3. Các chức năng đã xây dựng được:
## 3.1 Admin: 
- Chức năng đăng nhập, đăng xuất
- Chức năng thống kê đơn hàng theo từng ngày (dạng cột và bảng)
- Chức năng quản lý vai trò: thêm, xóa, sửa
- Chức năng quản lý quản trị viên: thêm, xóa, sửa, tìm kiếm theo tên và khóa/mở khóa tài khoản
- Chức năng quản lý nhân viên: thêm, xóa, sửa, tìm kiếm theo tên và khóa/mở khóa tài khoản
- Chức năng quản lý khách hàng: tìm kiếm theo tên và khóa/mở khóa tài khoản
- Chức năng quản lý sản phẩm: thêm, xóa, sửa, xem chi tiết, phân loại, tìm kiếm sản phẩm và phân trang
- Chức năng quản lý danh mục sản phẩm: thêm, xóa, sửa
- Chức năng quản lý bài viết: thêm, xóa, sửa và tìm kiếm
- Chức năng quản lý mã giảm giá: thêm, xóa, sửa, xem chi tiết
- Chức năng quản lý đơn hàng: phân loại, tìm kiếm, xem chi tiết, xuất hóa đơn và phân trang
- Chức năng quản lý liên hệ của khách hàng: tìm kiếm, xác nhận đã xử lý liên hệ của khách hàng và phân trang
  
## 3.2 Nhân viên:
- Chức năng đăng nhập, đăng xuất
- Chức năng thống kê đơn hàng theo từng ngày (dạng cột và bảng)
- Chức năng quản lý sản phẩm: thêm, sửa, xem chi tiết, phân loại, tìm kiếm sản phẩm và phân trang
- Chức năng quản lý danh mục sản phẩm: thêm, sửa
- Chức năng quản lý bài viết: thêm, sửa và tìm kiếm
- Chức năng quản lý mã giảm giá: thêm, sửa, xem chi tiết
- Chức năng quản lý đơn hàng: phân loại, tìm kiếm, xem chi tiết, xuất hóa đơn và phân trang
- Chức năng quản lý liên hệ của khách hàng: tìm kiếm, xác nhận đã xử lý liên hệ của khách hàng và phân trang

## 3.3 Khách hàng:
- Đăng nhập, đăng ký và đăng xuất
- Chức năng tìm kiếm, sắp xếp sản phẩm
- Chức năng lưu sản phẩm vào danh sách yêu thích
- Chức năng quản lý giỏ hàng: thêm, xóa, sửa
- Chức năng đặt hàng: thanh toán khi nhận hàng và thanh toán online, áp dụng mã giảm giá, gửi mail danh sách sản phẩm đã đặt sau khi đặt hàng thành công
- Chức năng quản lý đơn hàng: xem danh sách đơn hàng đã đặt và hủy hàng
- Chức năng bình luận 
- Chức năng gửi liên hệ cho quản trị viên

# 4. Giao diện và chức năng đã xây dựng
## 4.1 Khách hàng
### 4.1.1 Trang chủ
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/97d5ead2-4e10-4212-b634-dbb7ae853f7b)
##### Ở trang chủ của trang web sẽ hiển thị tất cả sản phẩm của cửa hàng. Ứng với mỗi sản phẩm có thể xem hình ảnh, tên, giá, giảm giá của sản phẩm. Ngoài ra còn có các chức năng như thêm sản phẩm vào danh sách yêu thích, thêm sản phẩm vào giỏ hàng

### 4.1.2 Phân loại sản phẩm
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/59613f18-8024-4542-bd31-18959f5494d1)
##### Khi hover chuột vào mục Sản phẩm sẽ hiển thị tất cả danh mục của hệ thống. Và bấm vào danh mục bất kì sẽ chuyển đến trang phân loại sản phẩm

![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/a81d21ab-720a-44a6-8c2b-af6634fcb234)

### 4.1.3 Sắp xếp sản phẩm
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/9eb717ba-5c51-4490-8f8b-e3158e82765b)
##### Phần sắp xếp sản phẩm có các mục như sắp xếp theo giá, sắp xếp theo tên sản phẩm và sắp xếp theo ngày đăng sản phẩm.

### 4.1.4 Tìm kiếm sản phẩm
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/b0fc4daa-509d-457d-94a5-f6ded368791a)
##### Khi điền từ khóa vào ô tìm kiếm thì trang web sẽ tự động gợi ý các sản phẩm có tên chứa từ khóa đó

![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/151d99f6-7320-45cb-a206-18247d570b38)
##### Khi click icon tìm kiếm hoặc nhấn enter thì trang web sẽ chuyển đến trang tìm kiếm và hiển thị kết quả tìm kiếm

### 4.1.5 Đăng ký
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/e0a30973-49ed-4f1a-a7c6-a7703169288c)
##### Trang Đăng Ký giúp người dùng đăng ký tài khoản của mình, khi khách hàng điền đầy đủ thông tin và click đăng ký thì hệ thống sẽ kiểm tra thông tin đã hợp lệ chưa, nếu hợp lệ sẽ thông báo đăng ký tài khoản thành công cho người dùng, nếu không hợp lệ sẽ hiện thông báo lý do không đăng ký tài khoản được. 
##### Những thông tin lưu ý khi đăng ký: Email phải là duy nhất, chưa đăng ký. Mật khẩu và nhập lại mật khẩu phải trùng khớp với nhau

### 4.1.6 Đăng nhập
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/bfa6c589-9e9c-4391-b557-cd1c91458340)
##### Trang Đăng Nhập sẽ cho phép người dùng đăng nhập vào tài khoản của mình để sử dụng những dịch vụ cần đăng nhập như bình luận, lưu sản phẩm, quản lý đơn hàng, đặt hàng,… nếu đăng nhập sai thì sẽ hiện thị lý do tại sao không đăng nhập được

### 4.1.7 Chi tiết sản phẩm
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/edcf32f0-7007-4d63-a61d-6d608d498406)
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/0e09fbe3-a169-41cc-96dd-2812d84e5e1b)

### 4.1.8 Bình luận và phản hồi
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/ce4f5542-86c3-4fb4-badd-b1bf08b56185)
##### Sau khi đăng nhập thì khách hàng có thể viết bình luận hoặc phản hồi

### 4.1.9 Danh sách yêu thích
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/1c500dc9-6ff4-460e-bdf8-a1b35ab14647)
##### Khi bấm vào icon trái tim thì sản phẩm sẽ được lưu vào danh sách yêu thích giúp cho khách hàng có thể xem lại những sản phẩm cần thiết
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/1f5983b5-a01d-4904-aeb6-1ab9354e2215)

### 4.1.10 Giỏ hàng
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/7d947ff8-19fb-4bfe-83ba-faf0647f9241)
##### Khi bấm vào icon giỏ hàng thì sản phẩm sẽ được đưa vào giỏ hàng. Trong phần giỏ hàng có thể xóa hoặc thay đổi số lượng của sản phẩm.

### 4.1.11 Thanh toán
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/9d4a4ca6-2a7a-4530-bb56-c5787f93edd7)
##### Trong phần thanh toán cần điền đầy đủ thông tin để thanh toán. Có 2 hình thức thanh toán: COD và Chuyển khoản

![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/31a08b8f-14cf-4276-9ab4-ed3041b2a6f2)
##### Trong phần thanh toán có thể sử dụng mã giảm giá: bằng cách nhập mã giảm giá ở ô giảm giá và bấm sử dụng. Hệ thống sẽ kiểm tra mã giảm giá đã đủ cách yêu cầu (về ngày bắt đầu, kết thúc, số lượng, giá trị tối thiểu của đơn hàng). Nếu hợp lệ thì sẽ tiến hành giảm giá theo giá trị của voucher

![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/2b39f13d-fa31-45f9-ba00-7569dd37fbe5)
##### Hình thức thanh toán online: Sau khi chọn ngân hàng thì sẽ chuyển đến trang thanh toán tương ứng với ngân hàng đó. Sau khi nhập thông tin thẻ và bấm tiếp tục sẽ chuyển đến trang xác thực OTP và thanh toán. Sau khi thanh toán thành công sẽ chuyển đến trang thanh toán thành công và hệ thống sẽ gửi đến email một thông báo về đơn hàng

![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/1204938a-97d6-4e53-973d-d2468d251e5d)
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/149128ec-7332-4c2b-b797-29ec2371b519)

### 4.1.12 Quản lý đơn hàng
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/3a8790f3-8de4-4423-9022-e205e46c11cd)
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/74bf0147-35b5-44b7-a00e-7298665ce8a7)

##### Trong phần danh sách đơn hàng, khách hàng có thể xem đơn hàng, hủy đơn hàng.
##### Để hủy đơn hàng cần bấm vào nút “Hủy” và nhấn “Có”, hệ thống sẽ tiến hành hủy đơn hàng

### 4.1.13 Bài viết
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/8a51b63a-de3b-4efd-b723-3211338c8889)

### 4.1.14 Gửi liên hệ cho admin (không cần đăng nhập)
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/ff91185c-2959-4a60-b586-125f5efa6450)

## 4.2 Quản trị viên và nhân viên
### 4.2.1 Đăng nhập
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/13e1c554-8b65-47bb-ab6d-e445ab673327)
##### Khi vào trang của admin cần phải đăng nhập bằng tài khoản của admin hoặc tài khoản nhân viên

### 4.2.2 Thống kê
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/2432460c-d3da-4820-be49-5868420dd89d)
##### Ở phần thống kê sẽ hiển thị tất cả đơn hàng, sản phẩm, khách hàng và mã giảm giá.
##### Ngoài ra còn hiển thị bảng thống kê doanh thu và lợi nhuận theo ngày

### 4.2.3 Quản lý vai trò
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/40ef6b56-71ee-4d93-9141-5b4321d9de86)
##### Ở phần quản lý vai trò, quản trị sẽ quản lý được các quyền và có thể thêm, xóa và sửa vai trò của hệ thống

### 4.2.4 Quản lý quản trị viên
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/c5b02fa3-1ba8-4aea-b0c4-287377e3d2fe)

##### Ở phần này chỉ có quản trị viên mới có quyền thêm, xóa, sửa, khóa và mở khóa tài khoản quản trị viên

### 4.2.5 Quản lý nhân viên
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/64504ecb-61e3-426f-a897-f9ab824aa89f)
##### Ở phần này chỉ có quản trị viên mới có quyền thêm, xóa, sửa, khóa và mở khóa tài khoản nhân viên

### 4.2.6 Quản lý khách hàng
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/27b46122-0bdd-4b9d-a787-56d7ff2d64da)
##### Ở phần này chỉ có quản trị viên mới có quyền mở/khóa tài khoản của khách hàng

### 4.2.7 Quản lý sản phẩm
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/a1ed7dc1-8725-4f26-9fbe-6e7a037ef121)
##### Ở phần quản lý sản phẩm, quản trị viên có thể thêm, xóa, sửa, xem chi tiết sản phẩm, phân loại sản phẩm theo danh mục, tìm kiếm sản phẩm theo tên và phân trang sản phẩm

### 4.2.8 Quản lý danh mục sản phẩm
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/746d1ed8-27de-48d5-b625-7318766a72a0)
##### Ở phần này, quản trị viên có thể thêm, xóa, sửa danh mục của sản phẩm

### 4.2.9 Quản lý bài viết
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/9c0394bc-8ed9-480a-b475-1e44f355aff3)
##### Ở phần này, quản trị viên có thể thêm, xóa, sửa, tìm kiếm bài viết và tất cả bài viết của hệ thống

### 4.2.10 Quản lý mã giảm giá
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/f606329c-ee0f-4e3d-a9f9-86d39f9d0fc8)
##### Ở phần này, quản trị viên có thể thêm, xóa, sửa, xem chi tiết mã giảm giá

### 4.2.11 Quản lý đơn hàng
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/3917b54f-3e1d-4071-96f2-55336c11644c)
##### Ở phần này, quản trị viên có thể tìm kiếm theo hình thức thanh toán, theo mã đơn hàng, theo tên người nhận hàng, theo số điện thoại, xem chi tiết và xuất hóa đơn của đơn hàng

![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/67ddcbdf-14d8-4cc2-b7c5-de98bc42e892)
##### Khi bấm vào xuất hóa đơn thì sẽ tải xuống một file excel về thông tin đơn hàng

### 4.2.12 Quản lý liên hệ của khách hàng
![image](https://github.com/lequanghao2002/FashionShop/assets/113456985/ec584656-7ccc-4fa9-b503-c316418a1789)
##### Ở phần này, quản trị viên dùng để trả lời những thông tin mà khách hàng gửi








