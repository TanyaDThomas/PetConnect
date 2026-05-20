# Pet Connect Testing

This document contains representative manual QA test cases performed during development and pre-deployment validation of the application.

---

## Authentication Tests

### AUTH-001 Register User
Steps:
1. Navigate to Register page
2. Enter valid credentials
3. Submit form

Expected Result:
- User account created
- Redirected to homepage

Status:
PASS

---

### AUTH-002 Invalid Password
Steps:
1. Enter incorrect password
2. Click Login

Expected Result:
- Error message shown
- User remains logged out

Status:
PASS

### AUTH-003 Successful Login (Valid Credentials)

Steps:

1. Navigate to Login page
2. Enter valid email + password
3. Submit form

Expected Result:

- User is authenticated
- Redirected to dashboard/homepage
- Session is created

Status:
PASS

---

### AUTH-004 Logout Functionality

Steps:

1. Log in successfully
2. Click Logout button

Expected Result:

-  session ends
- User redirected to login or public page
- Protected routes no longer accessible

Status: 
PASS


---

---

## Authorization Testing

### AUTHZ-001 Access Protected Route 

Steps:

1. Ensure user is logged out
2. Try visiting /payment or /animal/create directly via URL

Expected Result:

- Redirected to login page
- No access to protected content

Status:
PASS

---

### AUTHZ-002 Access Protected Route 

Steps:

1. Log in as valid user
2. Navigate to /animal/edit

Expected Result:

- Page loads normally
- User can access form

Status:
PASS

---

### AUTHZ-003 Manager Access Own Location

Steps:

1. Log in as Manager
2. Navigate Directly to /Admin/Index

Expected Result:

- Allowed Shelter Staff 
- Changes are saved successfully

Status:
PASS

---

### AUTHZ-004 Prevent Unauthorized Access

Steps:

1. Log in as staff
2. Navigate directly to /admin/index

Expected Result:

- Action blocked
- No data deleted

Status:
PASS

---

### AUTHZ-005 Block Editing Another User’s Resource

Steps:

1. Log in as User A
2. Attempt to edit User B’s staff (via UI or API request)

Expected Result:

- Action blocked
- No deletion occurs

Status:
PASS

---

### AUTHZ-006 Ensure Users Only See Their Own Data 

Steps:

1. Log in as User A
2. View animal listing page

Expected Result:

- Only User A’s shelter details are visible
- No data from other users appears

Status:
PASS

---
---

## Validation Testing

### VAL-001 Invalid Email Format

Steps:
1. Enter invalid email format during registration
2. Submit form

Expected Result:
- Email validation error shown

Status:
PASS

---

### VAL-002 Invalid Password Format

Steps:
1. Enter invalid password format during registration
2. Submit form

Expected Result:
- Email validation error shown

Status:
PASS

---

### VAL-003 Empty Registration Fields

Steps:

1. Leave email and password blank
2. Submit form

Expected Result:

- Required field errors shown
- Form not submitted

Status:
PASS

---
### VAL-004 Weak Password

Steps:

1. Enter password like 12345 or password
2. Submit form

Expected Result:

- Password strength error shown
- User prevented from registering

Status:
PASS 

---

### VAL-005 Password Mismatch

Steps:

1. Enter password: Test1234
2. Enter confirm password: Test5678
3. Submit

Expected Result:

- Error message shown
- Submission blocked

Status:
PASS

---
---

## CRUD Testing

### CRUD-001 Create Animal

Steps:
1. Navigate to Create Animal page
2. Fill out valid form
3. Submit

Expected Result:
- Animal saved successfully
- Animal appears in listings

Status:
PASS

---

### CRUD-002 Edit Animal

Steps:
1. Open existing Animal
2. Edit description
3. Save changes

Expected Result:
- Changes are persisted after save and visible on reload

Status:
PASS

---

### CRUD-003 Delete Animal

Steps:
1. Open existing animal
2. Delete animal
3. Confirm changes

Expected Result:
- Record is removed and no longer appears in listing

Status:
PASS

---

### CRUD-004 Create Adopter

Steps:
1. Navigate to Create Adopter page
2. Fill out valid form
3. Submit

Expected Result:
- Adopter saved successfully and appears in listings


Status:
PASS

---

### CRUD-005 Edit Adopter

Steps:
1. Open existing Adopter
2. Edit description
3. Save changes

Expected Result:
- Updated information persists after refresh

Status:
PASS


---

### CRUD-006 Delete Adopter

Steps:
1. Open existing adopter
2. Delete adopter
3. Confirm changes

Expected Result:
- Adopter is removed and no longer appears in listings

Status:
PASS

---

### CRUD-007 Create Adoption

Steps:
1. Navigate to Create Adoption page
2. Fill out valid form
3. Submit

Expected Result:
- Adoption saved successfully
- Adoption appears in listings

Status:
PASS

---

### CRUD-008 Create Payment

Steps:
1. Navigate to Create Payment page
2. Fill out valid form
3. Submit

Expected Result:
- Payment saved successfully
- Payment appears in listings

Status:
PASS

---

### CRUD-009 Create A Note

Steps:
1. Navigate to Create Animal page
2. Fill out valid form
3. Submit

Expected Result:
- Animal saved successfully
- Animal appears in listings

Status:
PASS

---

### CRUD-010 Create Shelter

Steps:
1. Sign in as Admin
2. Navigate to Create Shelter page
3. Fill out valid form
4. Submit

Expected Result:
- Shelter saved successfully
- Shelter appears in listings

Status:
PASS

---

### CRUD-011 Create Staff

Steps:
1. Sign in as Manager
2. Navigate to Create Staff page
3. Fill out valid form
4. Submit

Expected Result:
- Staff saved successfully and appears in listings

Status:
PASS

---


## Edge Case Testing


### EDGE-001 Extremely Long Animal Name

Steps:
1. Enter 101+ characters in name field
2. Submit form

Expected Result:
- Input handled gracefully
- Validation or truncation occurs

Actual Result:
- Validation message shown

Status:
PASS

---

### EDGE-002 Empty Form Submission

Steps:

1. Navigate to Create Animal form
2. Click Submit without entering any data

Expected Result:

- Validation errors shown for required fields
- No data submitted

Status:
PASS

---

### EDGE-003 Special Characters in Input

Steps:

1. Enter pet name like: !!!@@@###$$$
2. Submit form

Expected Result:

- Either validation error OR safe storage
- No app crash or UI break

Status:
PASS

---

### EDGE-004 Rapid Multiple Submissions (Spam Click)

Steps:

1. Click “Submit” button multiple times quickly

Expected Result:

- Only one request processed
- No duplicate entries created

Status:
PASS

---

### EDGE-005 Special Characters in Email/Name Fields

Steps:

1. Navigate to Create Adopter
2.Enter unusual characters in email fields (e.g. pet<>name@!!)
3. Submit form

Expected Result:

- Validation error OR sanitized input
- No backend errors

Status:
PASS


---

## Known Issues

### BUG-001 Mobile Navbar Layout
Issue:
- Navbar overlaps page on very small screens (<320px width). Is not mobile friendly and needs menu to collapse.

Impact:
- Major UI issue 

Planned Fix:
- Improve responsive CSS layout
- Add hamburger menu

Status:
Open


---

### BUG-006 No Pagination on Listings (if applicable)

Issue:
- All pets load at once instead of paginated results

Impact:
- Could affect performance with large datasets

Planned Fix:
- Implement server-side or client-side pagination for listings
- Limit initial query results

Status:
Open

---

### BUG-008 Refresh Resets Unsaved Form Data

Issue:
- When a user is filling out a form (e.g., Create Animal, Create Adopter), refreshing the browser page causes all entered form data to be lost.

Steps to Reproduce:

1. Navigate to a create/edit form (e.g., Create Animal)
2. Enter data into multiple fields
3. Refresh the browser page (F5 or Ctrl+R)

Expected Result:
- Form data should persist after refresh OR user should receive a warning before losing unsaved changes.

Actual Result:
- All entered form data is cleared and the form resets to default values.

Impact:

- Users may lose unsaved work unexpectedly
- Poor user experience for longer forms
- Higher risk of data re-entry frustration

Severity:
Medium (UX issue, not data corruption)

---


