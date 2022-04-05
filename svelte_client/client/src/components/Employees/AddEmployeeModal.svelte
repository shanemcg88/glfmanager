<script>
  import {
    Button,
    Modal,
    ModalBody,
    ModalFooter,
    ModalHeader
  } from 'sveltestrap';

    let open = false;

    export const toggle = () => (open = !open);

</script>

<div>
    <Modal isOpen={open} backdrop="static" centered={true} size="xl" {toggle}>
      <ModalHeader {toggle}>Create Employee</ModalHeader>
      <ModalBody>
        <form class="addEmployeeForm">

            <div class="firstRow">

              <div class="formLeftContainer">
                <div class="form-floating mb-3">
                    <input type="text" class="form-control" id="firstName" placeholder="First name">
                    <label for="firstName">First name</label>
                  </div>
                  <div class="form-floating mb-3">
                    <input type="text" class="form-control" id="lastName" placeholder="Last name">
                    <label for="lastName">Last name</label>
                  </div>
                  <div class="form-floating mb-3">
                    <input type="email" class="form-control" id="email" placeholder="Email">
                    <label for="email">Email</label>
                  </div>
                  <div class="form-floating mb-3">
                    <input type="tel" class="form-control" id="phone" placeholder="Phone number">
                    <label for="phone">Phone number</label>
                  </div>
              </div>

              <div class="formRightContainer">
                <div class="form-floating mb-3">
                  <input type="text" class="form-control" id="streetAddress" placeholder="Street address">
                  <label for="streetAddress">Street address</label>
                </div>
                <div class="form-floating mb-3">
                  <input type="text" class="form-control" id="city" placeholder="Postal code">
                  <label for="postalCode">Postal code</label>
                </div>
                <div class="form-floating mb-3">
                  <input type="text" class="form-control" id="province" placeholder="Province">
                  <label for="province">Province</label>
                </div>
                <div class="form-floating mb-3">
                  <input type="text" class="form-control" id="country" placeholder="Country">
                  <label for="country">Country</label>
                </div>
              </div>
  
            </div>

            <hr/>

            <div class="secondRow">
              <div class="form-floating">
                <textarea class="form-control" placeholder="Add skills here" id="skills" style="height: 100px"></textarea>
                <label for="skills">Skills</label>
              </div>
            </div>
        </form>
      </ModalBody>
      <ModalFooter>
        <Button color="primary" on:click={toggle}>Do Something</Button>
        <Button color="secondary" on:click={toggle}>Cancel</Button>
      </ModalFooter>
    </Modal>
</div>

<style>
  .firstRow {
    /* border: 1px solid green; */
    display: flex;
    justify-content: space-between;
  }
  .formLeftContainer {
    /* border:1px solid red; */
    width: 50%;
    padding: 5px;
  }
  .formRightContainer {
    /* border: 1px solid blue; */
    width: 50%;
    padding: 5px;
  }
  .secondRow {
  display: flex;
  }
</style>